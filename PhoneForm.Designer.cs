namespace MlSampleWindowCS
{
    partial class SIPPhoneForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIPPhoneForm));
            this.buttonRecordStartStop = new System.Windows.Forms.Button();
            this.buttonHoldRetrieve = new System.Windows.Forms.Button();
            this.buttonStartHangupCall = new System.Windows.Forms.Button();
            this.buttonTransfer = new System.Windows.Forms.Button();
            this.spkVolumeBar = new System.Windows.Forms.ProgressBar();
            this.micVolumeBar = new System.Windows.Forms.ProgressBar();
            this.spkVolume = new System.Windows.Forms.TrackBar();
            this.UInputLabel = new System.Windows.Forms.Label();
            this.muteSpeakerFlag = new System.Windows.Forms.CheckBox();
            this.muteMicrophoneFlag = new System.Windows.Forms.CheckBox();
            this.CallPanel = new System.Windows.Forms.GroupBox();
            this.DTFM6 = new System.Windows.Forms.Button();
            this.DTFMStar = new System.Windows.Forms.Button();
            this.DTFM2 = new System.Windows.Forms.Button();
            this.DTFM3 = new System.Windows.Forms.Button();
            this.DTFM9 = new System.Windows.Forms.Button();
            this.DTFM8 = new System.Windows.Forms.Button();
            this.DTFM1 = new System.Windows.Forms.Button();
            this.DTFM0 = new System.Windows.Forms.Button();
            this.DTFM5 = new System.Windows.Forms.Button();
            this.DTFMHash = new System.Windows.Forms.Button();
            this.DTFM4 = new System.Windows.Forms.Button();
            this.DTFM7 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.micVolume = new System.Windows.Forms.TrackBar();
            this.buttonPlayStartStop = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.AddressBox = new System.Windows.Forms.ComboBox();
            this.buttonJoin = new System.Windows.Forms.Button();
            this.groupBoxLines = new System.Windows.Forms.GroupBox();
            this.buttonLine5 = new System.Windows.Forms.Button();
            this.activeConnListbox = new System.Windows.Forms.ListBox();
            this.buttonLine6 = new System.Windows.Forms.Button();
            this.buttonLine1 = new System.Windows.Forms.Button();
            this.buttonLine4 = new System.Windows.Forms.Button();
            this.buttonLine3 = new System.Windows.Forms.Button();
            this.buttonLine2 = new System.Windows.Forms.Button();
            this.callDurationLabel = new System.Windows.Forms.Label();
            this.notificationsListBox = new System.Windows.Forms.ListBox();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkVoiceMailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unregisterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.çagrýListesiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.PictureErr = new System.Windows.Forms.Panel();
            this.pictureReceivedVideo = new System.Windows.Forms.PictureBox();
            this.pictureLocalVideo = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.buttonExpand = new System.Windows.Forms.LinkLabel();
            this.buttonSendText = new System.Windows.Forms.Button();
            this.navigationFrame1 = new DevExpress.XtraBars.Navigation.NavigationFrame();
            this.navigationPage1 = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.navigationPage2 = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.gridCallList = new DevExpress.XtraGrid.GridControl();
            this.ViewCallList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.spkVolume)).BeginInit();
            this.CallPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.micVolume)).BeginInit();
            this.groupBoxLines.SuspendLayout();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureReceivedVideo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLocalVideo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.navigationFrame1)).BeginInit();
            this.navigationFrame1.SuspendLayout();
            this.navigationPage1.SuspendLayout();
            this.navigationPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCallList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewCallList)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonRecordStartStop
            // 
            resources.ApplyResources(this.buttonRecordStartStop, "buttonRecordStartStop");
            this.buttonRecordStartStop.Name = "buttonRecordStartStop";
            this.buttonRecordStartStop.UseVisualStyleBackColor = true;
            this.buttonRecordStartStop.Click += new System.EventHandler(this.recordStartStopBtn_Click);
            // 
            // buttonHoldRetrieve
            // 
            resources.ApplyResources(this.buttonHoldRetrieve, "buttonHoldRetrieve");
            this.buttonHoldRetrieve.Name = "buttonHoldRetrieve";
            this.buttonHoldRetrieve.UseVisualStyleBackColor = true;
            this.buttonHoldRetrieve.Click += new System.EventHandler(this.holdRetreiveBtn_Click);
            // 
            // buttonStartHangupCall
            // 
            this.buttonStartHangupCall.FlatAppearance.BorderColor = System.Drawing.SystemColors.WindowFrame;
            resources.ApplyResources(this.buttonStartHangupCall, "buttonStartHangupCall");
            this.buttonStartHangupCall.Name = "buttonStartHangupCall";
            this.buttonStartHangupCall.UseVisualStyleBackColor = true;
            this.buttonStartHangupCall.Click += new System.EventHandler(this.callStartHangupBtn_Click);
            // 
            // buttonTransfer
            // 
            resources.ApplyResources(this.buttonTransfer, "buttonTransfer");
            this.buttonTransfer.Name = "buttonTransfer";
            this.buttonTransfer.UseVisualStyleBackColor = true;
            this.buttonTransfer.Click += new System.EventHandler(this.transferBtn_Click);
            // 
            // spkVolumeBar
            // 
            resources.ApplyResources(this.spkVolumeBar, "spkVolumeBar");
            this.spkVolumeBar.Maximum = 30000;
            this.spkVolumeBar.Name = "spkVolumeBar";
            // 
            // micVolumeBar
            // 
            resources.ApplyResources(this.micVolumeBar, "micVolumeBar");
            this.micVolumeBar.Maximum = 30000;
            this.micVolumeBar.Name = "micVolumeBar";
            // 
            // spkVolume
            // 
            resources.ApplyResources(this.spkVolume, "spkVolume");
            this.spkVolume.Maximum = 100;
            this.spkVolume.Name = "spkVolume";
            this.spkVolume.TickFrequency = 10;
            this.spkVolume.Scroll += new System.EventHandler(this.SpkVolume_Scroll);
            // 
            // UInputLabel
            // 
            resources.ApplyResources(this.UInputLabel, "UInputLabel");
            this.UInputLabel.Name = "UInputLabel";
            // 
            // muteSpeakerFlag
            // 
            resources.ApplyResources(this.muteSpeakerFlag, "muteSpeakerFlag");
            this.muteSpeakerFlag.Name = "muteSpeakerFlag";
            this.muteSpeakerFlag.UseVisualStyleBackColor = true;
            this.muteSpeakerFlag.CheckedChanged += new System.EventHandler(this.muteSoundCB_CheckedChanged);
            // 
            // muteMicrophoneFlag
            // 
            resources.ApplyResources(this.muteMicrophoneFlag, "muteMicrophoneFlag");
            this.muteMicrophoneFlag.Name = "muteMicrophoneFlag";
            this.muteMicrophoneFlag.UseVisualStyleBackColor = true;
            this.muteMicrophoneFlag.CheckedChanged += new System.EventHandler(this.muteMicCB_CheckedChanged);
            // 
            // CallPanel
            // 
            this.CallPanel.Controls.Add(this.DTFM6);
            this.CallPanel.Controls.Add(this.DTFMStar);
            this.CallPanel.Controls.Add(this.DTFM2);
            this.CallPanel.Controls.Add(this.DTFM3);
            this.CallPanel.Controls.Add(this.DTFM9);
            this.CallPanel.Controls.Add(this.DTFM8);
            this.CallPanel.Controls.Add(this.DTFM1);
            this.CallPanel.Controls.Add(this.DTFM0);
            this.CallPanel.Controls.Add(this.DTFM5);
            this.CallPanel.Controls.Add(this.DTFMHash);
            this.CallPanel.Controls.Add(this.DTFM4);
            this.CallPanel.Controls.Add(this.DTFM7);
            this.CallPanel.Controls.Add(this.muteMicrophoneFlag);
            this.CallPanel.Controls.Add(this.muteSpeakerFlag);
            this.CallPanel.Controls.Add(this.spkVolumeBar);
            this.CallPanel.Controls.Add(this.label1);
            this.CallPanel.Controls.Add(this.UInputLabel);
            this.CallPanel.Controls.Add(this.micVolumeBar);
            this.CallPanel.Controls.Add(this.micVolume);
            this.CallPanel.Controls.Add(this.spkVolume);
            resources.ApplyResources(this.CallPanel, "CallPanel");
            this.CallPanel.Name = "CallPanel";
            this.CallPanel.TabStop = false;
            // 
            // DTFM6
            // 
            resources.ApplyResources(this.DTFM6, "DTFM6");
            this.DTFM6.Name = "DTFM6";
            this.DTFM6.Tag = "6";
            this.DTFM6.UseVisualStyleBackColor = true;
            this.DTFM6.Click += new System.EventHandler(this.DTFM_Click);
            // 
            // DTFMStar
            // 
            resources.ApplyResources(this.DTFMStar, "DTFMStar");
            this.DTFMStar.Name = "DTFMStar";
            this.DTFMStar.Tag = "10";
            this.DTFMStar.UseVisualStyleBackColor = true;
            this.DTFMStar.Click += new System.EventHandler(this.DTFM_Click);
            // 
            // DTFM2
            // 
            resources.ApplyResources(this.DTFM2, "DTFM2");
            this.DTFM2.Name = "DTFM2";
            this.DTFM2.Tag = "2";
            this.DTFM2.UseVisualStyleBackColor = true;
            this.DTFM2.Click += new System.EventHandler(this.DTFM_Click);
            // 
            // DTFM3
            // 
            resources.ApplyResources(this.DTFM3, "DTFM3");
            this.DTFM3.Name = "DTFM3";
            this.DTFM3.Tag = "3";
            this.DTFM3.UseVisualStyleBackColor = true;
            this.DTFM3.Click += new System.EventHandler(this.DTFM_Click);
            // 
            // DTFM9
            // 
            resources.ApplyResources(this.DTFM9, "DTFM9");
            this.DTFM9.Name = "DTFM9";
            this.DTFM9.Tag = "9";
            this.DTFM9.UseVisualStyleBackColor = true;
            this.DTFM9.Click += new System.EventHandler(this.DTFM_Click);
            // 
            // DTFM8
            // 
            resources.ApplyResources(this.DTFM8, "DTFM8");
            this.DTFM8.Name = "DTFM8";
            this.DTFM8.Tag = "8";
            this.DTFM8.UseVisualStyleBackColor = true;
            this.DTFM8.Click += new System.EventHandler(this.DTFM_Click);
            // 
            // DTFM1
            // 
            resources.ApplyResources(this.DTFM1, "DTFM1");
            this.DTFM1.Name = "DTFM1";
            this.DTFM1.Tag = "1";
            this.DTFM1.UseVisualStyleBackColor = true;
            this.DTFM1.Click += new System.EventHandler(this.DTFM_Click);
            // 
            // DTFM0
            // 
            resources.ApplyResources(this.DTFM0, "DTFM0");
            this.DTFM0.Name = "DTFM0";
            this.DTFM0.Tag = "0";
            this.DTFM0.UseVisualStyleBackColor = true;
            this.DTFM0.Click += new System.EventHandler(this.DTFM_Click);
            // 
            // DTFM5
            // 
            resources.ApplyResources(this.DTFM5, "DTFM5");
            this.DTFM5.Name = "DTFM5";
            this.DTFM5.Tag = "5";
            this.DTFM5.UseVisualStyleBackColor = true;
            this.DTFM5.Click += new System.EventHandler(this.DTFM_Click);
            // 
            // DTFMHash
            // 
            resources.ApplyResources(this.DTFMHash, "DTFMHash");
            this.DTFMHash.Name = "DTFMHash";
            this.DTFMHash.Tag = "11";
            this.DTFMHash.UseVisualStyleBackColor = true;
            this.DTFMHash.Click += new System.EventHandler(this.DTFM_Click);
            // 
            // DTFM4
            // 
            resources.ApplyResources(this.DTFM4, "DTFM4");
            this.DTFM4.Name = "DTFM4";
            this.DTFM4.Tag = "4";
            this.DTFM4.UseVisualStyleBackColor = true;
            this.DTFM4.Click += new System.EventHandler(this.DTFM_Click);
            // 
            // DTFM7
            // 
            resources.ApplyResources(this.DTFM7, "DTFM7");
            this.DTFM7.Name = "DTFM7";
            this.DTFM7.Tag = "7";
            this.DTFM7.UseVisualStyleBackColor = true;
            this.DTFM7.Click += new System.EventHandler(this.DTFM_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // micVolume
            // 
            resources.ApplyResources(this.micVolume, "micVolume");
            this.micVolume.Maximum = 100;
            this.micVolume.Name = "micVolume";
            this.micVolume.TickFrequency = 10;
            this.micVolume.Scroll += new System.EventHandler(this.MicVolume_Scroll);
            // 
            // buttonPlayStartStop
            // 
            resources.ApplyResources(this.buttonPlayStartStop, "buttonPlayStartStop");
            this.buttonPlayStartStop.Name = "buttonPlayStartStop";
            this.buttonPlayStartStop.UseVisualStyleBackColor = true;
            this.buttonPlayStartStop.Click += new System.EventHandler(this.playStartStopButton_Click);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // AddressBox
            // 
            this.AddressBox.FormattingEnabled = true;
            resources.ApplyResources(this.AddressBox, "AddressBox");
            this.AddressBox.Name = "AddressBox";
            this.AddressBox.TextUpdate += new System.EventHandler(this.AddressBox_TextUpdate);
            this.AddressBox.TextChanged += new System.EventHandler(this.AddressBox_TextChanged);
            // 
            // buttonJoin
            // 
            resources.ApplyResources(this.buttonJoin, "buttonJoin");
            this.buttonJoin.Name = "buttonJoin";
            this.buttonJoin.UseVisualStyleBackColor = true;
            this.buttonJoin.Click += new System.EventHandler(this.joinBtn_Click);
            // 
            // groupBoxLines
            // 
            this.groupBoxLines.Controls.Add(this.buttonLine5);
            this.groupBoxLines.Controls.Add(this.activeConnListbox);
            this.groupBoxLines.Controls.Add(this.buttonLine6);
            this.groupBoxLines.Controls.Add(this.buttonLine1);
            this.groupBoxLines.Controls.Add(this.buttonLine4);
            this.groupBoxLines.Controls.Add(this.buttonLine3);
            this.groupBoxLines.Controls.Add(this.buttonLine2);
            this.groupBoxLines.Controls.Add(this.callDurationLabel);
            resources.ApplyResources(this.groupBoxLines, "groupBoxLines");
            this.groupBoxLines.Name = "groupBoxLines";
            this.groupBoxLines.TabStop = false;
            // 
            // buttonLine5
            // 
            resources.ApplyResources(this.buttonLine5, "buttonLine5");
            this.buttonLine5.Name = "buttonLine5";
            this.buttonLine5.UseVisualStyleBackColor = true;
            this.buttonLine5.Click += new System.EventHandler(this.lineBtn_Click);
            // 
            // activeConnListbox
            // 
            this.activeConnListbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.activeConnListbox.FormattingEnabled = true;
            resources.ApplyResources(this.activeConnListbox, "activeConnListbox");
            this.activeConnListbox.Name = "activeConnListbox";
            this.activeConnListbox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.activeConnListbox_MouseDoubleClick);
            // 
            // buttonLine6
            // 
            resources.ApplyResources(this.buttonLine6, "buttonLine6");
            this.buttonLine6.Name = "buttonLine6";
            this.buttonLine6.UseVisualStyleBackColor = true;
            this.buttonLine6.Click += new System.EventHandler(this.lineBtn_Click);
            // 
            // buttonLine1
            // 
            resources.ApplyResources(this.buttonLine1, "buttonLine1");
            this.buttonLine1.Name = "buttonLine1";
            this.buttonLine1.UseVisualStyleBackColor = true;
            this.buttonLine1.Click += new System.EventHandler(this.lineBtn_Click);
            // 
            // buttonLine4
            // 
            resources.ApplyResources(this.buttonLine4, "buttonLine4");
            this.buttonLine4.Name = "buttonLine4";
            this.buttonLine4.UseVisualStyleBackColor = true;
            this.buttonLine4.Click += new System.EventHandler(this.lineBtn_Click);
            // 
            // buttonLine3
            // 
            resources.ApplyResources(this.buttonLine3, "buttonLine3");
            this.buttonLine3.Name = "buttonLine3";
            this.buttonLine3.UseVisualStyleBackColor = true;
            this.buttonLine3.Click += new System.EventHandler(this.lineBtn_Click);
            // 
            // buttonLine2
            // 
            resources.ApplyResources(this.buttonLine2, "buttonLine2");
            this.buttonLine2.Name = "buttonLine2";
            this.buttonLine2.UseVisualStyleBackColor = true;
            this.buttonLine2.Click += new System.EventHandler(this.lineBtn_Click);
            // 
            // callDurationLabel
            // 
            resources.ApplyResources(this.callDurationLabel, "callDurationLabel");
            this.callDurationLabel.Name = "callDurationLabel";
            // 
            // notificationsListBox
            // 
            this.notificationsListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.notificationsListBox.FormattingEnabled = true;
            resources.ApplyResources(this.notificationsListBox, "notificationsListBox");
            this.notificationsListBox.Name = "notificationsListBox";
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.aboutToolStripMenuItem,
            this.checkVoiceMailToolStripMenuItem,
            this.unregisterToolStripMenuItem,
            this.çagrýListesiToolStripMenuItem});
            this.menuStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            resources.ApplyResources(this.menuStrip, "menuStrip");
            this.menuStrip.Name = "menuStrip";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            resources.ApplyResources(this.settingsToolStripMenuItem, "settingsToolStripMenuItem");
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            resources.ApplyResources(this.aboutToolStripMenuItem, "aboutToolStripMenuItem");
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // checkVoiceMailToolStripMenuItem
            // 
            this.checkVoiceMailToolStripMenuItem.Name = "checkVoiceMailToolStripMenuItem";
            resources.ApplyResources(this.checkVoiceMailToolStripMenuItem, "checkVoiceMailToolStripMenuItem");
            this.checkVoiceMailToolStripMenuItem.Click += new System.EventHandler(this.checkVoiceMailToolStripMenuItem_Click);
            // 
            // unregisterToolStripMenuItem
            // 
            this.unregisterToolStripMenuItem.Name = "unregisterToolStripMenuItem";
            resources.ApplyResources(this.unregisterToolStripMenuItem, "unregisterToolStripMenuItem");
            this.unregisterToolStripMenuItem.Click += new System.EventHandler(this.unregisterToolStripMenuItem_Click);
            // 
            // çagrýListesiToolStripMenuItem
            // 
            this.çagrýListesiToolStripMenuItem.Name = "çagrýListesiToolStripMenuItem";
            resources.ApplyResources(this.çagrýListesiToolStripMenuItem, "çagrýListesiToolStripMenuItem");
            this.çagrýListesiToolStripMenuItem.Click += new System.EventHandler(this.çagrýListesiToolStripMenuItem_Click);
            // 
            // pictureBox3
            // 
            resources.ApplyResources(this.pictureBox3, "pictureBox3");
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.TabStop = false;
            // 
            // PictureErr
            // 
            resources.ApplyResources(this.PictureErr, "PictureErr");
            this.PictureErr.Name = "PictureErr";
            // 
            // pictureReceivedVideo
            // 
            this.pictureReceivedVideo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.pictureReceivedVideo, "pictureReceivedVideo");
            this.pictureReceivedVideo.Name = "pictureReceivedVideo";
            this.pictureReceivedVideo.TabStop = false;
            // 
            // pictureLocalVideo
            // 
            this.pictureLocalVideo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.pictureLocalVideo, "pictureLocalVideo");
            this.pictureLocalVideo.Name = "pictureLocalVideo";
            this.pictureLocalVideo.TabStop = false;
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // buttonExpand
            // 
            this.buttonExpand.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.buttonExpand, "buttonExpand");
            this.buttonExpand.LinkColor = System.Drawing.Color.Blue;
            this.buttonExpand.Name = "buttonExpand";
            this.buttonExpand.TabStop = true;
            this.buttonExpand.Click += new System.EventHandler(this.expandFormBtn_Click);
            // 
            // buttonSendText
            // 
            resources.ApplyResources(this.buttonSendText, "buttonSendText");
            this.buttonSendText.Name = "buttonSendText";
            this.buttonSendText.UseVisualStyleBackColor = true;
            this.buttonSendText.Click += new System.EventHandler(this.sendTextBtn_Click);
            // 
            // navigationFrame1
            // 
            this.navigationFrame1.Controls.Add(this.navigationPage1);
            this.navigationFrame1.Controls.Add(this.navigationPage2);
            resources.ApplyResources(this.navigationFrame1, "navigationFrame1");
            this.navigationFrame1.Name = "navigationFrame1";
            this.navigationFrame1.Pages.AddRange(new DevExpress.XtraBars.Navigation.NavigationPageBase[] {
            this.navigationPage1,
            this.navigationPage2});
            this.navigationFrame1.SelectedPage = this.navigationPage1;
            // 
            // navigationPage1
            // 
            resources.ApplyResources(this.navigationPage1, "navigationPage1");
            this.navigationPage1.Controls.Add(this.pictureReceivedVideo);
            this.navigationPage1.Controls.Add(this.pictureBox3);
            this.navigationPage1.Controls.Add(this.pictureLocalVideo);
            this.navigationPage1.Name = "navigationPage1";
            // 
            // navigationPage2
            // 
            resources.ApplyResources(this.navigationPage2, "navigationPage2");
            this.navigationPage2.Controls.Add(this.gridCallList);
            this.navigationPage2.Name = "navigationPage2";
            // 
            // gridCallList
            // 
            resources.ApplyResources(this.gridCallList, "gridCallList");
            this.gridCallList.MainView = this.ViewCallList;
            this.gridCallList.Name = "gridCallList";
            this.gridCallList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.ViewCallList});
            // 
            // ViewCallList
            // 
            this.ViewCallList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3});
            this.ViewCallList.GridControl = this.gridCallList;
            this.ViewCallList.Name = "ViewCallList";
            this.ViewCallList.OptionsView.ShowGroupPanel = false;
            this.ViewCallList.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.ViewCallList_RowCellClick);
            this.ViewCallList.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.ViewCallList_RowStyle);
            // 
            // gridColumn1
            // 
            resources.ApplyResources(this.gridColumn1, "gridColumn1");
            this.gridColumn1.FieldName = "TIPI";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn2
            // 
            resources.ApplyResources(this.gridColumn2, "gridColumn2");
            this.gridColumn2.FieldName = "NUMARA";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn3
            // 
            resources.ApplyResources(this.gridColumn3, "gridColumn3");
            this.gridColumn3.FieldName = "DURUM";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            // 
            // SIPPhoneForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.navigationFrame1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.buttonExpand);
            this.Controls.Add(this.PictureErr);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.notificationsListBox);
            this.Controls.Add(this.groupBoxLines);
            this.Controls.Add(this.AddressBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.buttonJoin);
            this.Controls.Add(this.buttonSendText);
            this.Controls.Add(this.buttonTransfer);
            this.Controls.Add(this.buttonPlayStartStop);
            this.Controls.Add(this.buttonRecordStartStop);
            this.Controls.Add(this.CallPanel);
            this.Controls.Add(this.buttonHoldRetrieve);
            this.Controls.Add(this.buttonStartHangupCall);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.Name = "SIPPhoneForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SIPPhoneForm_FormClosing);
            this.Load += new System.EventHandler(this.SIPPhone_Load);
            ((System.ComponentModel.ISupportInitialize)(this.spkVolume)).EndInit();
            this.CallPanel.ResumeLayout(false);
            this.CallPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.micVolume)).EndInit();
            this.groupBoxLines.ResumeLayout(false);
            this.groupBoxLines.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureReceivedVideo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLocalVideo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.navigationFrame1)).EndInit();
            this.navigationFrame1.ResumeLayout(false);
            this.navigationPage1.ResumeLayout(false);
            this.navigationPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridCallList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewCallList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        #endregion

		private System.Windows.Forms.Button buttonStartHangupCall;
		private System.Windows.Forms.PictureBox pictureBox3;
		private System.Windows.Forms.Button buttonRecordStartStop;
		private System.Windows.Forms.Button buttonHoldRetrieve;
		private System.Windows.Forms.Button buttonTransfer;
		private System.Windows.Forms.CheckBox muteMicrophoneFlag;
		private System.Windows.Forms.CheckBox muteSpeakerFlag;
		private System.Windows.Forms.Label UInputLabel;
		private System.Windows.Forms.TrackBar spkVolume;
		private System.Windows.Forms.ProgressBar micVolumeBar;
		private System.Windows.Forms.ProgressBar spkVolumeBar;
		private System.Windows.Forms.GroupBox CallPanel;
		private System.Windows.Forms.TrackBar micVolume;
		private System.Windows.Forms.Button buttonPlayStartStop;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ComboBox AddressBox;
		private System.Windows.Forms.Button buttonJoin;
		private System.Windows.Forms.GroupBox groupBoxLines;
		private System.Windows.Forms.ListBox activeConnListbox;
		private System.Windows.Forms.Button buttonLine1;
		private System.Windows.Forms.Button buttonLine3;
		private System.Windows.Forms.Button buttonLine6;
		private System.Windows.Forms.Button buttonLine4;
		private System.Windows.Forms.Button buttonLine2;
		private System.Windows.Forms.Button buttonLine5;
		private System.Windows.Forms.ListBox notificationsListBox;
		private System.Windows.Forms.MenuStrip menuStrip;
		private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button DTFM6;
		private System.Windows.Forms.Button DTFMStar;
		private System.Windows.Forms.Button DTFM2;
		private System.Windows.Forms.Button DTFM3;
		private System.Windows.Forms.Button DTFM9;
		private System.Windows.Forms.Button DTFM8;
		private System.Windows.Forms.Button DTFM1;
		private System.Windows.Forms.Button DTFM0;
		private System.Windows.Forms.Button DTFM5;
		private System.Windows.Forms.Button DTFMHash;
		private System.Windows.Forms.Button DTFM4;
		private System.Windows.Forms.Button DTFM7;
		private System.Windows.Forms.Panel PictureErr;
		private System.Windows.Forms.PictureBox pictureReceivedVideo;
		private System.Windows.Forms.PictureBox pictureLocalVideo;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.LinkLabel buttonExpand;
		private System.Windows.Forms.Button buttonSendText;
        private System.Windows.Forms.ToolStripMenuItem unregisterToolStripMenuItem;
		private System.Windows.Forms.Label callDurationLabel;
		private System.Windows.Forms.ToolStripMenuItem checkVoiceMailToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem çagrýListesiToolStripMenuItem;
        private DevExpress.XtraBars.Navigation.NavigationFrame navigationFrame1;
        private DevExpress.XtraBars.Navigation.NavigationPage navigationPage1;
        private DevExpress.XtraBars.Navigation.NavigationPage navigationPage2;
        private DevExpress.XtraGrid.GridControl gridCallList;
        private DevExpress.XtraGrid.Views.Grid.GridView ViewCallList;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
    }
}

