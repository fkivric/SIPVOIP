using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
//using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Threading;
using SIPVoipSDK;
using System.Data.SqlClient;
using System.Data;

namespace MlSampleWindowCS
{

    using ConnectionInfo = KeyValuePair<int, string>;
    using ConnectionsTbl = Dictionary<int, string>;

    public partial class SIPPhoneForm : Form
    {
        public class CallList
        {
            public int CallType { get; set; }
            public string CallNumber { get; set; }
            public string CallRecall { get; set; }
            public string CallUser { get; set; }
        }
        SqlConnection sql = new SqlConnection(Properties.Settings.Default.ConnectionString);
        DbQuery conn = new DbQuery();
        public class ConnListBoxItem
        {
            public int handle;
            public string connection;

            public ConnListBoxItem(int _handle, string _connection)
            {
                handle = _handle;
                connection = _connection;
            }

            public override string ToString()
            {
                return this.connection;
            }
        }

        public class LineInfo
        {
            public LineInfo(int id)
            {
                m_id = id;
                m_conn = new ConnectionsTbl();
                m_bCalling = false;
                m_bCallEstablished = false;
                m_bCallHeld = false;
                m_bCallPlayStarted = false;
                m_usrInputStr = "";
            }

            public ConnectionsTbl m_conn;
            public int m_id;
            public int m_lastConnId;
            public bool m_bCalling;
            public bool m_bCallEstablished;
            public bool m_bCallHeld;
            public bool m_bCallPlayStarted;
            public string m_usrInputStr;
            public System.Windows.Forms.Timer m_callDurationTimer;
            public TimeSpan m_callTime;
            public string m_callTimeStr;
        }

        private CAbtoPhone AbtoPhone = new CAbtoPhone();

        private string cfgFileName = "phoneCfg.ini";

        public const int LineCount = 6;
        private int m_curLineId = 1;

        ArrayList m_lineConnections = new ArrayList();

        private int m_lineIdWhereRecStarted = 0;

        private bool m_MP3RecordingEnabled = false;
        private bool m_AutoAnswerEnabled = false;

        public List<CallList> GetCallLists = new List<CallList>();
        public SIPPhoneForm()
        {
            InitializeComponent();
        }


        //////////////////////////////////////
        //Utils

        private LineInfo GetCurLine()
        {
            return (LineInfo)m_lineConnections[m_curLineId - 1];
        }

        private LineInfo GetLine(int lineId)
        {
            return (LineInfo)m_lineConnections[lineId - 1];
        }

        private void DisplayConnectionsAll(LineInfo lnInfo)
        {
            activeConnListbox.Items.Clear();
            foreach (ConnectionInfo it in lnInfo.m_conn) DisplayConnection(it);
        }


        private void DisplayConnection(ConnectionInfo ci)
        {
            int itemIndex = activeConnListbox.Items.Add(new ConnListBoxItem(ci.Key, ci.Value));
            activeConnListbox.SelectedIndex = itemIndex;
        }

        private void RemoveConnection(int connectionId)
        {
            foreach (ConnListBoxItem t in activeConnListbox.Items)
            {
                if (t.handle == connectionId) { activeConnListbox.Items.Remove(t); break; }
            }

            int count = activeConnListbox.Items.Count;
            if (count >= 1) activeConnListbox.SelectedIndex = count - 1;
        }

        private bool GetSelectedConnection(out int connectionId)
        {
            //Check connections count
            connectionId = 0;
            int count = activeConnListbox.Items.Count;
            if (count == 0) return false;

            //Get sel connection
            int selectedIndex = activeConnListbox.SelectedIndex;
            if (selectedIndex == -1) selectedIndex = count - 1;

            //Get conn id
            connectionId = ((ConnListBoxItem)activeConnListbox.Items[selectedIndex]).handle;
            return true;
        }


        private void ChageControlsState(LineInfo li)
        {
            ChageLineCaption(li);

            buttonStartHangupCall.Text = li.m_bCallEstablished || li.m_bCalling ? "Telefonu Kapat" : "Çaðrý Baþlat";

            buttonHoldRetrieve.Visible = li.m_bCallEstablished;
            buttonHoldRetrieve.Text = li.m_bCallHeld ? "Geri al" : "Tutun";

            buttonTransfer.Visible = li.m_bCallEstablished;
            buttonJoin.Visible = li.m_bCallEstablished;

            callDurationLabel.Visible = li.m_bCallEstablished;
            callDurationLabel.Text = li.m_callTimeStr;

            AddressBox.Enabled = li.m_bCallEstablished || li.m_bCalling ? false : true;

            buttonPlayStartStop.Text = li.m_bCallPlayStarted ? "Oynat Durdur" : "Dosya Oynat";
            buttonRecordStartStop.Text = (m_lineIdWhereRecStarted != 0) ? "Kaydý Durdur" : "Kaydet";

            UInputLabel.Text = li.m_usrInputStr;
        }


        private void ChageLineCaption(LineInfo li)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Hat");
            sb.Append(li.m_id);
            if (li.m_bCallEstablished) sb.Insert(0, "[Aktif]");

            getLineButton(li.m_id).Text = sb.ToString();

        }

        private Button getLineButton(int lineId)
        {
            switch (lineId)
            {
                case 1: return buttonLine1;
                case 2: return buttonLine2;
                case 3: return buttonLine3;
                case 4: return buttonLine4;
                case 5: return buttonLine5;
                case 6: return buttonLine6;
            }
            return buttonLine1;
        }

        private void HighlightCurLine(int prevCurLine, int newCurLine)
        {
            Button prevBut = getLineButton(prevCurLine);
            Button newBut = getLineButton(newCurLine);

            prevBut.Font = new Font(prevBut.Font.FontFamily, prevBut.Font.Size, prevBut.Font.Style ^ FontStyle.Bold);
            newBut.Font = new Font(newBut.Font.FontFamily, newBut.Font.Size, newBut.Font.Style | FontStyle.Bold);
        }


        private void displayNotifyMsg(string msg)
        {
            notificationsListBox.Items.Add(msg);
            notificationsListBox.TopIndex = notificationsListBox.Items.Count - 1;//scrol down
        }

        string sorgu = @"select case when CallID = 1 then 'YAPILAN ARAMA' else 'GELEN ARAMA' end as TIPI,CallNumber as NUMARA, case when CallRecall = 0 then 'Cevapsýz' else 'Arandý' end DURUM from CallList 
            left outer join CallType on CallID = CallType
            where CallUser = '{0}'";
        private void SIPPhone_Load(object sender, EventArgs e)
        {
            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
            {
                System.Deployment.Application.ApplicationDeployment ad = System.Deployment.Application.ApplicationDeployment.CurrentDeployment;
                this.Text = "Version : " + ad.CurrentVersion.Major + "." + ad.CurrentVersion.Minor + "." + ad.CurrentVersion.Build + "." + ad.CurrentVersion.Revision;

            }
            if (this.Text == "")
            {
                //lblversion2.Text = Application.ProductVersion;
                this.Text = "Version : " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
            //Displ initial status
            displayNotifyMsg("Baðlanýyor...");

            //Configure phone
            bool bRes = ConfigurePhone();
            if (!bRes)
            {
                PictureErr.Top = 0;
                PictureErr.Left = CallPanel.Left;
                PictureErr.Width = CallPanel.Width;
                PictureErr.Height = CallPanel.Top + CallPanel.Height;
                PictureErr.BorderStyle = 0;
                PictureErr.Visible = true;
                return;
            }


            //Make lines
            for (int i = 1; i <= LineCount; ++i) m_lineConnections.Add(new LineInfo(i));
            buttonLine1.Tag = 1; buttonLine2.Tag = 2; buttonLine3.Tag = 3;
            buttonLine4.Tag = 4; buttonLine5.Tag = 5; buttonLine6.Tag = 6;

            //Configure playback controls
            spkVolume.SetRange(0, 100);
            spkVolume.Value = AbtoPhone.PlaybackVolume;
            spkVolume.TickFrequency = 10;

            spkVolumeBar.Minimum = 0;
            spkVolumeBar.Maximum = Int16.MaxValue;

            //Configure record controls
            micVolume.SetRange(0, 100);
            micVolume.Value = AbtoPhone.RecordVolume;
            micVolume.TickFrequency = 10;

            micVolumeBar.Minimum = 0;
            micVolumeBar.Maximum = Int16.MaxValue;

            //Address box
            AddressBox.Items.Add("music@iptel.org");
            AddressBox.SelectedIndex = 0;

            //Mute Speaker/Microphone
            muteSpeakerFlag.Checked = true;
            muteMicrophoneFlag.Checked = true;

            //Get state
            m_MP3RecordingEnabled = AbtoPhone.Config.MP3RecordingEnabled != 0;
            m_AutoAnswerEnabled = AbtoPhone.Config.AutoAnswerEnabled != 0;

            AcceptButton = buttonStartHangupCall;

            expandFormBtn_Click(this, null);
            sorgu = String.Format(sorgu, loginuser);
            gridCallList.DataSource = DbQuery.Query(sorgu, Properties.Settings.Default.ConnectionString);
            //CallList call = new CallList();
            //call.CallNumber = AddressBox.Text;
            //call.CallType = 2;
            //call.CallUser = loginuser;
            //call.CallRecall = "0";
            //conn.InsertCallList(call, sql);

        }//SIPPhone_Load
        public static string loginuser = "";

        protected bool ConfigurePhone()
        {
            //Assign event handlers
            this.AbtoPhone.OnInitialized += new _IAbtoPhoneEvents_OnInitializedEventHandler(this.AbtoPhone_OnInitialized);
            this.AbtoPhone.OnLineSwiched += new _IAbtoPhoneEvents_OnLineSwichedEventHandler(this.AbtoPhone_OnLineSwiched);
            this.AbtoPhone.OnEstablishedCall += new _IAbtoPhoneEvents_OnEstablishedCallEventHandler(this.AbtoPhone_OnEstablishedCall);
            this.AbtoPhone.OnIncomingCall += new _IAbtoPhoneEvents_OnIncomingCallEventHandler(this.AbtoPhone_OnIncomingCall);
            this.AbtoPhone.OnClearedCall += new _IAbtoPhoneEvents_OnClearedCallEventHandler(this.AbtoPhone_OnClearedCall);
            this.AbtoPhone.OnVolumeUpdated += new _IAbtoPhoneEvents_OnVolumeUpdatedEventHandler(this.AbtoPhone_OnVolumeUpdated);
            this.AbtoPhone.OnRegistered += new _IAbtoPhoneEvents_OnRegisteredEventHandler(this.AbtoPhone_OnRegistered);
            this.AbtoPhone.OnPlayFinished += new _IAbtoPhoneEvents_OnPlayFinishedEventHandler(this.AbtoPhone_OnPlayFinished);
            this.AbtoPhone.OnEstablishedConnection += new _IAbtoPhoneEvents_OnEstablishedConnectionEventHandler(this.AbtoPhone_OnEstablishedConnection);
            this.AbtoPhone.OnClearedConnection += new _IAbtoPhoneEvents_OnClearedConnectionEventHandler(this.AbtoPhone_OnClearedConnection);
            this.AbtoPhone.OnToneReceived += new _IAbtoPhoneEvents_OnToneReceivedEventHandler(this.AbtoPhone_OnToneReceived);
            this.AbtoPhone.OnTextMessageReceived += new _IAbtoPhoneEvents_OnTextMessageReceivedEventHandler(this.AbtoPhone_OnTextMessageReceived);
            this.AbtoPhone.OnTextMessageSentStatus += new _IAbtoPhoneEvents_OnTextMessageSentStatusEventHandler(AbtoPhone_OnTextMessageSentStatus);
            this.AbtoPhone.OnPhoneNotify += new _IAbtoPhoneEvents_OnPhoneNotifyEventHandler(this.AbtoPhone_OnPhoneNotify);
            this.AbtoPhone.OnRemoteAlerting2 += new _IAbtoPhoneEvents_OnRemoteAlerting2EventHandler(AbtoPhone_OnRemoteAlerting2);
            this.AbtoPhone.OnSubscribeStatus += new _IAbtoPhoneEvents_OnSubscribeStatusEventHandler(AbtoPhone_OnSubscribeStatus);
            this.AbtoPhone.OnSubscriptionNotify += new _IAbtoPhoneEvents_OnSubscriptionNotifyEventHandler(AbtoPhone_OnSubscriptionNotify);
            //this.AbtoPhone.OnDetectedAnswerTime+= new _IAbtoPhoneEvents_OnDetectedAnswerTimeEventHandler(this.AbtoPhone_OnDetectedAnswerTime);
            //this.AbtoPhone.OnRecordFinished +=new _IAbtoPhoneEvents_OnRecordFinishedEventHandler(AbtoPhone_OnRecordFinished);
            //this.AbtoPhone.OnReceivedRequestInfo += new _IAbtoPhoneEvents_OnReceivedRequestInfoEventHandler(AbtoPhone_OnReceivedRequestInfo);
            //AbtoPhone.OnToneDetected += new _IAbtoPhoneEvents_OnToneDetectedEventHandler(AbtoPhone_OnToneDetected);
            //this.AbtoPhone.OnPlayFinished2 += new _IAbtoPhoneEvents_OnPlayFinished2EventHandler(AbtoPhone_OnPlayFinished2);
            //AbtoPhone.OnHoldCall2 += new _IAbtoPhoneEvents_OnHoldCall2EventHandler(AbtoPhone_OnHoldCall2);
            //AbtoPhone.OnReceivedRequestUpdate += new _IAbtoPhoneEvents_OnReceivedRequestUpdateEventHandler(AbtoPhone_OnReceivedRequestUpdate);

            //Get config
            CConfig phoneCfg = AbtoPhone.Config;

            //Load config values from file
            phoneCfg.Load(cfgFileName);

            //phoneCfg.ExSipAccount_Add("192.168.0.150", "216", "216", "", "", 300, 1, 0);
            //phoneCfg.ExSipAccount_Add("192.168.0.150", "217", "217", "", "", 300, 1, 0);



            //Manual set needed values
            //phoneCfg.ListenPort = 5060;
            //phoneCfg.StunServer = "stun.l.google.com:19302";			


            //phoneCfg.RegDomain = "...";//your domain
            //phoneCfg.RegUser = "..";//your user name
            //phoneCfg.RegPass = "..";//your password
            //phoneCfg.RegExpire = 300;
            //phoneCfg.TlsVerifyServerEnabled = false;//Disable server certificate verification (always trust)
            //phoneCfg.SignallingTransport = (int)TransportType.eTransportTLSv1;//Set TLS transport
            //phoneCfg.EncryptedCallEnabled = 1;//Enable SRTP

            //Specify License key
            //phoneCfg.LicenseUserId = "{Trial1f95-8887-147A-4C015A39...}";
            //phoneCfg.LicenseKey = "{w6uQzP8gZos82bmoIQ4zxMEt+ecv4bj+...}";		

            //Log level
            //phoneCfg.LogLevel= (LogLevelType)11;// LogLevelType.eLogDebug;//eLogError//(LogLevelType)11;
            //phoneCfg.LogPath = "C:\\temp\\logs";

            //Set AdditionalDnsServer as google dns
            //phoneCfg.AdditionalDnsServer = "8.8.8.8";

            //Specify network interface
            //phoneCfg.ActiveNetworkInterface = "...";
            //phoneCfg.TonesTypesToDetect = (int)ToneType.eToneMF + (int)ToneType.eToneDtmf;
            //phoneCfg.SignallingTransport = (int)TransportType.eTransportTLSv1;		
            //phoneCfg.SignallingTransport = (int)TransportType.eTransportUDP;
            //phoneCfg.SignallingTransport = (int)TransportType.eTransportTCP;
            //phoneCfg.TlsVerifyServerEnabled = false;
            //phoneCfg.EncryptedCallEnabled = 1;


            //phoneCfg.RegDomain = "x.x.x.x:5061";//typical port number for TLS transport on server side is 5061
            //phoneCfg.TonesTypesToDetect = (int)ToneType.eToneDtmf;


            //Set video windows
            //phoneCfg.RemoteVideoWindow = pictureReceivedVideo.Handle.ToInt32();
            //phoneCfg.LocalVideoWindow = pictureLocalVideo.Handle.ToInt32();
            //phoneCfg.RemoteVideoWindow_Add(pictureReceivedVideo.Handle.ToInt32());

            //phoneCfg->put_AudioQosDscpVal(40);
            //phoneCfg->put_VideoQosDscpVal(50);
            //phoneCfg.TonesTypesToDetect = (int)ToneType.eToneDtmf;
            //phoneCfg.DtmfAsSipInfoEnabled = 1;

            //... other initializations	
            //phoneCfg.MixerFilePlayerEnabled = 0;//allows play few files simultaneously


            try
            {
                //Apply modified config
                AbtoPhone.ApplyConfig();


                AbtoPhone.Initialize();
                loginuser = phoneCfg.RegAuthId;
            }
            catch (Exception e)
            {
                displayNotifyMsg(e.Message);
                return false;
            }

            return true;
        }

        ///////////////////////////////////
        //Phone control


        private void callStartHangupBtn_Click(object sender, EventArgs e)
        {
            LineInfo lnInfo = GetCurLine();

            if (lnInfo.m_bCallEstablished || lnInfo.m_bCalling)
            {
                //HangUp
                int connectionId;
                if (GetSelectedConnection(out connectionId) == true)
                {
                    AbtoPhone.HangUp(connectionId);
                }
                else
                {
                    AbtoPhone.HangUpLastCall();
                }
                gridCallList.DataSource = DbQuery.Query(sorgu, Properties.Settings.Default.ConnectionString);
            }
            else
            {
                //Get addr
                string address = AddressBox.Text;
                if (address.Length == 0) return;

                //Append addr to combo
                int idx = AddressBox.FindString(address, -1);
                if (idx == -1) AddressBox.Items.Add(address);

                //Set status
                displayNotifyMsg("Calling...");

                //Set flag, Cange controls state
                lnInfo.m_bCalling = true;
                ChageControlsState(lnInfo);

                //Start call without video
                //AbtoPhone.Config.VideoCallEnabled = 0;
                //AbtoPhone.ApplyConfig();
                //Start call and get assigned connectionId
                int connId = AbtoPhone.StartCall2(address);
                var call = new CallList();
                call.CallNumber = address;
                call.CallType = 1;
                call.CallUser = loginuser;
                call.CallRecall = "1";
                var sonuc = DbQuery.GetValue($"SELECT [CallNumber] FROM [CallCenter].[dbo].[CallList] where CallNumber = '{AddressBox.Text}'", Properties.Settings.Default.ConnectionString);
                if (sonuc != AddressBox.Text)
                {
                    conn.InsertCallList(call, sql);
                }
                //gridCallList.DataSource = DbQuery.Query(sorgu, Properties.Settings.Default.ConnectionString);
            }

        }//callStartHangupButton_Click


        private void holdRetreiveBtn_Click(object sender, EventArgs e)
        {
            LineInfo lnInfo = GetCurLine();

            //Hold/retrieve
            AbtoPhone.HoldRetrieveCall(lnInfo.m_id);

            //Change button caption
            buttonHoldRetrieve.Text = lnInfo.m_bCallHeld ? "Hold" : "Retrieve";

            //Change flag
            lnInfo.m_bCallHeld = !lnInfo.m_bCallHeld;
        }

        private void playStartStopButton_Click(object sender, EventArgs e)
        {
            stopStartPlaying(false, m_curLineId);
        }

        private void recordStartStopBtn_Click(object sender, EventArgs e)
        {
            LineInfo lnInfo = GetCurLine();
            if (m_lineIdWhereRecStarted != 0)
            {
                AbtoPhone.StopRecording();
                buttonRecordStartStop.Text = "Rec";
                displayNotifyMsg("Recording stopped");
                m_lineIdWhereRecStarted = 0;
            }
            else
            {
                SaveFileDialog fileDlg = new SaveFileDialog();
                fileDlg.Filter = (m_MP3RecordingEnabled == true) ? "Sound Files (*.mp3)|*.mp3" : "Sound Files (*.wav)|*.wav";
                fileDlg.OverwritePrompt = true;
                if (fileDlg.ShowDialog(this) != DialogResult.OK) return;

                AbtoPhone.StartRecording(fileDlg.FileName);
                buttonRecordStartStop.Text = "RecStop";
                displayNotifyMsg("Recording file: " + fileDlg.FileName);

                m_lineIdWhereRecStarted = m_curLineId;
            }
        }

        private void transferBtn_Click(object sender, EventArgs e)
        {
            //Get transfer addr
            TransferAddrForm dlg = new TransferAddrForm();
            if (dlg.ShowDialog(this) != DialogResult.OK) return;

            //Transfer call
            AbtoPhone.TransferCall(dlg.textBoxAddr.Text);
        }

        private void joinBtn_Click(object sender, EventArgs e)
        {
            //Get line Id
            JoinForm dlg = new JoinForm();
            dlg.m_curLineId = m_curLineId;
            dlg.m_selLineId = 0;
            if (dlg.ShowDialog(this) != DialogResult.OK) return;

            //Check same line
            if (dlg.m_selLineId == m_curLineId) return;

            //Check selected line
            LineInfo selLineInfo = GetLine(dlg.m_selLineId);
            LineInfo curLineInfo = GetCurLine();
            if (!selLineInfo.m_bCallEstablished) return;

            //Append
            foreach (ConnectionInfo it in selLineInfo.m_conn) curLineInfo.m_conn.Add(it.Key, it.Value);
            selLineInfo.m_conn.Clear();

            //Displ
            DisplayConnectionsAll(curLineInfo);

            //Join
            AbtoPhone.JoinToCurrentCall(dlg.m_selLineId);
        }

        private void lineBtn_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            int lineId = (int)b.Tag;

            AbtoPhone.SetCurrentLine(lineId);
        }

        private void DTFM_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            bool bDtmfSent = false;

            while (!bDtmfSent)
            {
                try
                {
                    //AbtoPhone.SendTone(b.Text);
                    AbtoPhone.SendToneEx(Convert.ToInt32(b.Tag), 200, 1, 1, 0);
                    bDtmfSent = true;
                }
                catch (Exception)
                {
                    Thread.Sleep(100);
                }
            }

        }

        private void MicVolume_Scroll(object sender, EventArgs e)
        {
            AbtoPhone.RecordVolume = micVolume.Value;
        }

        private void SpkVolume_Scroll(object sender, EventArgs e)
        {
            AbtoPhone.PlaybackVolume = spkVolume.Value;
        }

        private void muteSoundCB_CheckedChanged(object sender, EventArgs e)
        {
            AbtoPhone.PlaybackMuted = muteSpeakerFlag.Checked ? 0 : 1;
        }

        private void muteMicCB_CheckedChanged(object sender, EventArgs e)
        {
            AbtoPhone.RecordMuted = muteMicrophoneFlag.Checked ? 0 : 1;
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Get cfg values
            CConfig phoneCfg = AbtoPhone.Config;

            //Create and show dlg
            ConfigForm cfgDlg = new ConfigForm();
            cfgDlg.phoneCfg = phoneCfg;
            cfgDlg.m_defLoadStorePath = AbtoPhone.SDKPath();
            cfgDlg.m_versionStr = AbtoPhone.RetrieveVersion();
            cfgDlg.m_addrStr = AbtoPhone.RetrieveExternalAddress();

            if (cfgDlg.ShowDialog(this) != DialogResult.OK)
            {
                AbtoPhone.CancelConfig();
                return;
            }

            //Apply new values
            AbtoPhone.ApplyConfig();

            //Store
            phoneCfg.Store(cfgFileName);

            //Get state
            m_MP3RecordingEnabled = phoneCfg.MP3RecordingEnabled != 0;
            m_AutoAnswerEnabled = phoneCfg.AutoAnswerEnabled != 0;
        }


        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm dlg = new AboutForm();
            dlg.ShowDialog(this);
        }

        private void webPageLabel_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.voipsipsdk.com");
        }


        private void activateSDK_Click(object sender, EventArgs e)
        {
            //Show form
            ActivationForm dlg = new ActivationForm();
            if (dlg.ShowDialog(this) != DialogResult.OK) return;

            //Store cfg values
            CConfig phoneCfg = AbtoPhone.Config;
            phoneCfg.LicenseUserId = dlg.m_licUserId;
            phoneCfg.LicenseKey = dlg.m_licKey;
            phoneCfg.Store(cfgFileName);

            //Message box
            MessageBox.Show("Restart application for finalize activation.");
        }


        private void stopStartPlaying(bool bCalledByPlayFinishedEvent, int lineId)
        {
            LineInfo lnInfo = GetLine(lineId);

            //If called by PlayFinished Event and playing has already stopped - do nothing
            if (bCalledByPlayFinishedEvent && !lnInfo.m_bCallPlayStarted) return;

            if (lnInfo.m_bCallPlayStarted)
            {
                AbtoPhone.StopPlayback();
                if (lineId == m_curLineId) buttonPlayStartStop.Text = "Play";
            }
            else
            {
                OpenFileDialog fileDlg = new OpenFileDialog();
                fileDlg.Multiselect = false;
                fileDlg.Filter = "Sound Files (*.wav)|*.wav|Sound Files (*.mp3)|*.mp3";
                if (fileDlg.ShowDialog(this) != DialogResult.OK) return;

                int succeded = AbtoPhone.PlayFile(fileDlg.FileName);
                if (succeded == 0) return;

                displayNotifyMsg("Playing file: " + fileDlg.FileName);
                buttonPlayStartStop.Text = "PlayStop";
            }

            lnInfo.m_bCallPlayStarted = !lnInfo.m_bCallPlayStarted;

        }//stopStartPlaying


        //////////////////////////////////////////////////////////////
        //Events
        delegate void ActivateSDK_Delegate(object sender, EventArgs e);

        private void AbtoPhone_OnInitialized(string Msg)
        {
            displayNotifyMsg(Msg);

            if (Msg.Contains("FAILED"))
                BeginInvoke(new ActivateSDK_Delegate(activateSDK_Click), this, null);
        }


        private void AbtoPhone_OnLineSwiched(int lineId)
        {
            //Display line as pressed button
            HighlightCurLine(m_curLineId, lineId);

            //Remember
            m_curLineId = lineId;

            //Display connections of cur line
            LineInfo lnInfo = GetCurLine();
            DisplayConnectionsAll(lnInfo);

            //Show/Hide call controls
            ChageControlsState(lnInfo);
        }

        private void AbtoPhone_OnEstablishedCall(string adress, int lineId)
        {
            //Update line state
            LineInfo lnInfo = GetLine(lineId);
            lnInfo.m_usrInputStr = "";
            lnInfo.m_bCallEstablished = true;
            lnInfo.m_bCalling = false;

            //Start call duration timer
            startCallDurationTimer(lnInfo);

            //Update controls (only when it's cur line event)
            if (lineId == m_curLineId)
            {
                //Display status
                displayNotifyMsg(adress);

                //Cange controls state
                ChageControlsState(lnInfo);
            }
            else
            {
                ChageLineCaption(lnInfo);
            }
        }


        private void AbtoPhone_OnIncomingCall(string adress, int lineId)
        {
            if (m_AutoAnswerEnabled == true) return;

            //bool bIsVideo = AbtoPhone.HasSIPBodyVideoMedia(lineId) != 0;

            //Show form
            IncomingForm dlg = new IncomingForm();
            dlg.textBoxCaller.Text = adress;
            dlg.textBoxLine.Text = "Line" + lineId.ToString();

            //AbtoPhone.Config.VideoCallEnabled = 0;//Answer audio or video
            //AbtoPhone.ApplyConfig();

            if (dlg.ShowDialog(this) == DialogResult.Yes) AbtoPhone.AnswerCallLine(lineId);
            else AbtoPhone.RejectCallLine(lineId);
            gridCallList.DataSource = DbQuery.Query(sorgu, Properties.Settings.Default.ConnectionString);
        }

        private void AbtoPhone_OnClearedCall(string Msg, int status, int lineId)
        {
            //Update line state
            LineInfo lnInfo = GetLine(lineId);
            lnInfo.m_usrInputStr = "";
            lnInfo.m_bCallEstablished = false;
            lnInfo.m_bCalling = false;
            lnInfo.m_callTimeStr = "";
            if (lnInfo.m_callDurationTimer != null) lnInfo.m_callDurationTimer.Stop();

            //Update controls (only when it's cur line event)
            if (lineId == m_curLineId)
            {
                //Display status
                displayNotifyMsg(Msg + ". Status: " + status.ToString());

                //Cange controls state
                ChageControlsState(lnInfo);
            }
            else
            {
                ChageLineCaption(lnInfo);
            }
        }

        private void AbtoPhone_OnToneReceived(int tone, int connectionId, int lineId)
        {
            LineInfo lnInfo = GetLine(lineId);

            StringBuilder sb = new StringBuilder();
            sb.Append(lnInfo.m_usrInputStr);
            sb.Append((char)tone);
            lnInfo.m_usrInputStr = sb.ToString();

            if (lineId == m_curLineId) UInputLabel.Text = lnInfo.m_usrInputStr;
        }

        private void AbtoPhone_OnVolumeUpdated(int IsMicrophone, int level)
        {
            if (IsMicrophone == 0) spkVolumeBar.Value = level;
            else micVolumeBar.Value = level;
        }

        private void AbtoPhone_OnRegistered(string Msg)
        {
            displayNotifyMsg(Msg);
        }

        private void AbtoPhone_OnPlayFinished(string Msg)
        {
            string playStr = "Play Finished on Line: ";

            int idx = Msg.IndexOf(playStr);
            if (idx == 0)
            {
                string lineStr = Msg.Substring(playStr.Length);
                stopStartPlaying(true, int.Parse(lineStr));
            }

            displayNotifyMsg(Msg);
        }


        private void AbtoPhone_OnEstablishedConnection(string addrFrom, string addrTo, int connectionId, int lineId)
        {
            LineInfo lnInfo = GetLine(lineId);
            string addr = lnInfo.m_bCalling ? addrTo : addrFrom;

            lnInfo.m_conn.Add(connectionId, addr);
            lnInfo.m_lastConnId = connectionId;

            if (lineId == m_curLineId) DisplayConnection(new ConnectionInfo(connectionId, addr));
        }

        private void AbtoPhone_OnClearedConnection(int connectionId, int lineId)
        {
            LineInfo lnInfo = GetLine(lineId);
            lnInfo.m_conn.Remove(connectionId);
            lnInfo.m_lastConnId = 0;

            if (lineId == m_curLineId) RemoveConnection(connectionId);
        }


        private void AbtoPhone_OnTextMessageReceived(string from, string message)
        {
            displayNotifyMsg("'" + message + "' received from: " + from);
        }

        private void AbtoPhone_OnTextMessageSentStatus(string address, string reason, int bSuccess)
        {
            if (bSuccess != 0) displayNotifyMsg("Message sent successfully to " + address + " Reason: " + reason);
            else displayNotifyMsg("Message sent failure to " + address + " Reason: " + reason);
        }

        private void AbtoPhone_OnPhoneNotify(string message)
        {
            displayNotifyMsg(message);

            //"Redirect Success. Connection: x";
            //"Redirect Failure. Connection: x Status y";
            Match match = Regex.Match(message, @"Redirect.*Connection: \d+");
            if (match.Success)
            {
                string connIdStr = Regex.Match(match.Value, @"\d+").Value;
                AbtoPhone.HangUp(int.Parse(connIdStr));
            }
        }

        private void AbtoPhone_OnRemoteAlerting2(int ConnectionId, int lineid, int responseCode, string reasonMsg)
        {
            string str = "Remote alerting: " + responseCode.ToString() + " " + reasonMsg;
            displayNotifyMsg(str);
        }

        //void AbtoPhone_OnToneDetected(ToneType tType, string ToneStr, int ConnectionId, int LineId)
        //{
        //if (tType == ToneType.eToneMF)
        //{
        //	if(ToneStr=="450");
        //	if(ToneStr=="480");
        //	if(ToneStr=="620");
        //	if(ToneStr=="AnsweringMachine T-Mobile 1250");
        //	if(ToneStr=="AnsweringMachine Verizon 1500");
        //	if(ToneStr=="AnsweringMachine Sprint/Nextel 1400");
        //	if(ToneStr=="AnsweringMachine AT&T/Cingular 1700");
        //	if (ToneStr == "FAX") ;
        //}
        //}


        private void activeConnListbox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //Get sel connection
            int connectionId;
            if (GetSelectedConnection(out connectionId) != true) return;

            //Display form
            ContributionForm dlg = new ContributionForm();
            if (dlg.ShowDialog(this) != DialogResult.OK) return;

            //Modify
            AbtoPhone.SetConnectionContribution(connectionId, dlg.m_inputGain, dlg.m_outputGain);
        }

        private void expandFormBtn_Click(object sender, EventArgs e)
        {
            const int deltaWidth = 7;
            if (this.Width > CallPanel.Width + deltaWidth)
            {
                //Straiten
                this.Width = CallPanel.Width + deltaWidth;
                buttonExpand.Text = ">>";
            }
            else
            {
                //Expand
                this.Width = pictureReceivedVideo.Width + pictureReceivedVideo.Left + deltaWidth;
                buttonExpand.Text = "<<";
            }

            pictureBox1.Left = groupBoxLines.Right - pictureBox1.Width;
            buttonExpand.Left = this.Width - deltaWidth - buttonExpand.Width;
            //webPageLabel.Width = buttonExpand.Left - deltaWidth;
        }

        private void sendTextBtn_Click(object sender, EventArgs e)
        {
            //Display form
            SendTextForm dlg = new SendTextForm();
            dlg.address = AddressBox.Text;

            //Check return result - send message
            if ((dlg.ShowDialog(this) == DialogResult.OK) &&
               (dlg.address.Length != 0) && (dlg.message.Length != 0))
                AbtoPhone.SendTextMessage(dlg.address, dlg.message, 0);
        }

        private void unregisterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.AbtoPhone.Config.RegExpire = 0;
            this.AbtoPhone.ApplyConfig();
        }

        private void startCallDurationTimer(LineInfo lnInfo)
        {
            if (lnInfo.m_callDurationTimer == null)
            {
                lnInfo.m_callDurationTimer = new System.Windows.Forms.Timer();
                lnInfo.m_callDurationTimer.Tick += new EventHandler(OnCallDurationTimerEvent);
                lnInfo.m_callDurationTimer.Tag = lnInfo.m_id;
                lnInfo.m_callDurationTimer.Interval = 1000;
            }

            lnInfo.m_callTime = new TimeSpan(0, 0, 0);
            lnInfo.m_callTimeStr = "Call time: 00:00:00";

            lnInfo.m_callDurationTimer.Start();
        }


        private void OnCallDurationTimerEvent(object sender, EventArgs e)
        {
            //Get timer
            System.Windows.Forms.Timer timer = (System.Windows.Forms.Timer)sender;
            if (sender == null) return;
            int lineId = (int)timer.Tag;

            //Get line info
            LineInfo lnInfo = GetLine(lineId);
            if (lnInfo == null) return;

            //Increment duration
            lnInfo.m_callTime = lnInfo.m_callTime.Add(new TimeSpan(0, 0, 1));
            lnInfo.m_callTimeStr = "Call time: " + lnInfo.m_callTime.ToString();

            //Display current duration
            if (lineId == m_curLineId) callDurationLabel.Text = lnInfo.m_callTimeStr;
        }


        //Play/Record buf source code example
        private void playBufToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TestPlayBuf t = new TestPlayBuf();
            //AbtoPhone.PlayBuffer(ref t.buf[0], t.buf.Length, 8000);

            //		long subscriptionId = AbtoPhone.Subscriptions.SubscribeCustomEvent("call-info", "150");
        }

        /*
        const int recordSeconds = 10;
        int TestRecordBufSamples = 0, TestRecordBufBytes=0;
        IntPtr TestRecordBuf = IntPtr.Zero;

        private void recordBufToolStripMenuItem_Click(object sender, EventArgs e)
        {		
            if (TestRecordBuf == IntPtr.Zero)
            {
                TestRecordBufSamples = AbtoPhone.Config.SamplesPerSecond * recordSeconds ;
                TestRecordBufBytes = TestRecordBufSamples * Marshal.SizeOf(typeof(Int16));
                TestRecordBuf = Marshal.AllocHGlobal(TestRecordBufBytes);
            }

            AbtoPhone.StartRecordingBuffer((long)TestRecordBuf, TestRecordBufBytes);
        }

        void AbtoPhone_OnRecordFinished(string Msg)
        {
            //Verify
            if (TestRecordBuf == IntPtr.Zero) return;

            //Copy recorded buf
            //Int16[] destArr = new Int16[TestRecordBufSamples];
            //Marshal.Copy(TestRecordBuf, destArr, 0, TestRecordBufSamples);

            byte[] destArr = new byte[TestRecordBufBytes];
            Marshal.Copy(TestRecordBuf, destArr, 0, TestRecordBufBytes);

            MemoryStream ms = new MemoryStream(TestRecordBufBytes);
            ms.Write(destArr, 0, TestRecordBufBytes);

            //Start recording next chunk
            AbtoPhone.StartRecordingBuffer((long)TestRecordBuf, TestRecordBufBytes);

            //Handle recorded


            FileStream file = new FileStream("recorded_buf.txt", FileMode.Create, FileAccess.Write);
            ms.WriteTo(file);
            file.Close();
            ms.Close();


    // 		using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"recorded_buf.txt"))
    // 		{
    // 			StringBuilder bldr = new StringBuilder();
    // 			for (int i = 0; i < TestRecordBufSamples; ++i)
    // 			{
    // 				bldr.Append("0x");
    // 				bldr.Append(destArr[i].ToString("X2"));
    // 				bldr.Append(",");
    // 				if((i % 12)==0 && i>0)
    // 				{
    // 					file.WriteLine(bldr.ToString());
    // 					bldr = new StringBuilder();
    // 				}
    // 			}
    // 	}
        }
         */

        private void SIPPhoneForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (TestRecordBuf != IntPtr.Zero) Marshal.FreeHGlobal(TestRecordBuf);
        }


        private void checkVoiceMailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Subscribe for events
            int subscriptionId = AbtoPhone.Subscriptions.SubscribeMessageSummary();
        }

        void AbtoPhone_OnSubscribeStatus(int subscriptionId, int statusCode, string statusMsg)
        {
            string str = string.Format("OnVoiceMail: Not supported. StatusCode: {0}", statusCode);
            displayNotifyMsg(str);
        }

        void AbtoPhone_OnSubscriptionNotify(int subscriptionId, string StateStr, string NotifyStr)
        {
            string str = string.Format("OnVoiceMail: {0}", StateStr);
            displayNotifyMsg(str);
        }

        private void AddressBox_TextChanged(object sender, EventArgs e)
        {
            AddressBox.Text = AddressBox.Text.Replace(" ", "");
        }

        private void AddressBox_TextUpdate(object sender, EventArgs e)
        {
            AddressBox.Text = AddressBox.Text.Replace(" ", "");
        }

        private void çagrýListesiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const int deltaWidth = 10;
            if (this.Width > CallPanel.Width + deltaWidth)
            {
                //Straiten
                navigationFrame1.SelectedPage = navigationPage1;
                this.Width = CallPanel.Width + deltaWidth;
            }
            else
            {
                //Expand
                navigationFrame1.SelectedPage = navigationPage2;
                this.Width = navigationFrame1.Width + navigationFrame1.Left + deltaWidth;
            }

            pictureBox1.Left = groupBoxLines.Right - pictureBox1.Width;
            buttonExpand.Left = this.Width - deltaWidth - buttonExpand.Width;

        }

        private void ViewCallList_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (ViewCallList.DataRowCount > 0)
            {
                if (ViewCallList.GetRowCellValue(e.RowHandle, "DURUM") != null)
                {
                    var renkKodu = ViewCallList.GetRowCellValue(e.RowHandle, "DURUM").ToString();
                    if (renkKodu == "Cevapsýz")
                    {
                        e.Appearance.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff0000");
                        e.Appearance.BackColor2 = System.Drawing.ColorTranslator.FromHtml("#ff0000");
                    }
                    else
                    {
                        e.Appearance.BackColor = System.Drawing.ColorTranslator.FromHtml("#fff ");
                        e.Appearance.BackColor2 = System.Drawing.ColorTranslator.FromHtml("#fff ");
                    }
                }
            }
        }

        private void ViewCallList_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks >1 && e.RowHandle >= 0)
            {
                var Durum = ViewCallList.GetRowCellValue(e.RowHandle, "DURUM").ToString();
                var Number = ViewCallList.GetRowCellValue(e.RowHandle, "NUMARA").ToString();
                if (Durum == "Cevapsýz")
                {
                    AddressBox.Text = Number;
                    callStartHangupBtn_Click(null, null);
                }
                else
                {
                    MessageBox.Show("Aranan Numarayý aramak istiyorsunuz");
                }
            }
        }
    }//class SIPPhone

}//namespace MlSampleWindowCS
