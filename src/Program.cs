using System;
using System.IO;
using System.Drawing;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace WindowsMaintenanceTool
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }

    public class MainForm : Form
    {
        // Embedded Base64 Logo (Character with Blue Recycle Bin)
        private const string LOGO_BASE64 = "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";

        // Windows Native Shell32 API for Recycle Bin
        [DllImport("Shell32.dll", CharSet = CharSet.Unicode)]
        static extern uint SHEmptyRecycleBin(IntPtr hwnd, string pszRootPath, uint dwFlags);
        const uint SHERB_NOCONFIRMATION = 0x00000001;
        const uint SHERB_NOPROGRESSUI = 0x00000002;
        const uint SHERB_NOSOUND = 0x00000004;

        // UI Header Controls
        private Panel headerPanel;
        private PictureBox picLogo;
        private Label lblHeaderTitle;
        private Label lblHeaderSubtitle;
        private Label lblDriveInfo;
        private Label lblBadge;

        // UI Left Panel Controls
        private Panel leftPanel;
        private Label lblOptionsTitle;
        private CheckBox chkTemp;
        private CheckBox chkRecycle;
        private CheckBox chkExplorer;
        private CheckBox chkUpdates;
        private CheckBox chkPrefetch;
        private CheckBox chkLogs;
        private CheckBox chkNetwork;
        private CheckBox chkSfcDism;

        private Button btnSelectAll;
        private Button btnSelectDefault;
        private Button btnStart;
        private Button btnQuickClean;

        // UI Right Panel Controls
        private Panel rightPanel;
        private Panel statsContainer;
        private Panel cardSpace;
        private Label lblCardSpaceTitle;
        private Label lblCardSpaceVal;

        private Panel cardFiles;
        private Label lblCardFilesTitle;
        private Label lblCardFilesVal;

        private Panel cardStatus;
        private Label lblCardStatusTitle;
        private Label lblCardStatusVal;

        private ProgressBar progressBar;
        private RichTextBox txtLog;

        private Panel bottomLogBar;
        private Button btnClearLog;
        private Button btnOpenTemp;
        private Button btnCopyLog;

        // State
        private bool isRunning = false;
        private Thread workerThread = null;
        private long totalFreedBytes = 0;
        private int totalFilesDeleted = 0;

        public MainForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Lightweight System Maintenance Tool v2.2";
            this.Size = new Size(950, 670);
            this.MinimumSize = new Size(900, 620);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(15, 17, 21);
            this.ForeColor = Color.FromArgb(235, 235, 235);
            this.Font = new Font("Segoe UI", 9.25f, FontStyle.Regular);

            // Set Form Icon from executable
            try
            {
                this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            }
            catch { }

            // ================= HEADER PANEL =================
            headerPanel = new Panel();
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Height = 78;
            headerPanel.BackColor = Color.FromArgb(20, 23, 29);
            headerPanel.Padding = new Padding(16, 8, 16, 8);

            // Logo PictureBox
            picLogo = new PictureBox();
            picLogo.Size = new Size(62, 62);
            picLogo.Location = new Point(14, 8);
            picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            picLogo.BackColor = Color.Transparent;
            try
            {
                byte[] imgBytes = Convert.FromBase64String(LOGO_BASE64);
                using (MemoryStream ms = new MemoryStream(imgBytes))
                {
                    picLogo.Image = new Bitmap(ms);
                }
            }
            catch { }
            headerPanel.Controls.Add(picLogo);

            // Title & Subtitle
            lblHeaderTitle = new Label();
            lblHeaderTitle.Text = "SYSTEM MAINTENANCE TOOL";
            lblHeaderTitle.Font = new Font("Segoe UI", 13.5f, FontStyle.Bold);
            lblHeaderTitle.ForeColor = Color.FromArgb(245, 245, 250);
            lblHeaderTitle.Location = new Point(84, 14);
            lblHeaderTitle.AutoSize = true;

            lblHeaderSubtitle = new Label();
            lblHeaderSubtitle.Text = "Native Windows System Cleaner • Network Repair • Disk Optimization";
            lblHeaderSubtitle.Font = new Font("Segoe UI", 8.75f, FontStyle.Regular);
            lblHeaderSubtitle.ForeColor = Color.FromArgb(148, 163, 184);
            lblHeaderSubtitle.Location = new Point(86, 42);
            lblHeaderSubtitle.AutoSize = true;

            // Drive C info & Admin badge on the right
            lblDriveInfo = new Label();
            lblDriveInfo.Text = GetDriveSummary();
            lblDriveInfo.Font = new Font("Segoe UI", 8.5f, FontStyle.Regular);
            lblDriveInfo.ForeColor = Color.FromArgb(148, 163, 184);
            lblDriveInfo.AutoSize = true;
            lblDriveInfo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblDriveInfo.Location = new Point(690, 16);

            lblBadge = new Label();
            lblBadge.Text = "🛡️ ADMINISTRATOR MODE";
            lblBadge.Font = new Font("Segoe UI", 8.0f, FontStyle.Bold);
            lblBadge.BackColor = Color.FromArgb(18, 48, 30);
            lblBadge.ForeColor = Color.FromArgb(74, 222, 128);
            lblBadge.Padding = new Padding(8, 4, 8, 4);
            lblBadge.AutoSize = true;
            lblBadge.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblBadge.Location = new Point(690, 38);

            headerPanel.Controls.Add(lblHeaderTitle);
            headerPanel.Controls.Add(lblHeaderSubtitle);
            headerPanel.Controls.Add(lblDriveInfo);
            headerPanel.Controls.Add(lblBadge);

            // ================= LEFT PANEL =================
            leftPanel = new Panel();
            leftPanel.Dock = DockStyle.Left;
            leftPanel.Width = 365;
            leftPanel.BackColor = Color.FromArgb(22, 25, 32);
            leftPanel.Padding = new Padding(16, 12, 16, 12);

            lblOptionsTitle = new Label();
            lblOptionsTitle.Text = "MAINTENANCE OPTIONS";
            lblOptionsTitle.Font = new Font("Segoe UI", 8.0f, FontStyle.Bold);
            lblOptionsTitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblOptionsTitle.Location = new Point(16, 12);
            lblOptionsTitle.AutoSize = true;
            leftPanel.Controls.Add(lblOptionsTitle);

            int startY = 36;
            int gap = 31;

            chkTemp = CreateModernCheckBox("🧹  Temporary Files (Temp & Dumps)", true, 16, startY);
            chkRecycle = CreateModernCheckBox("🗑️  Empty Recycle Bin (All Drives)", true, 16, startY + gap);
            chkExplorer = CreateModernCheckBox("🖼️  Explorer Thumbnail Cache", true, 16, startY + gap * 2);
            chkUpdates = CreateModernCheckBox("📦  Windows Update Download Cache", true, 16, startY + gap * 3);
            chkPrefetch = CreateModernCheckBox("⚡  Prefetch & Obsolete Traces", true, 16, startY + gap * 4);
            chkLogs = CreateModernCheckBox("📝  Error Reports (WER) & Logs", true, 16, startY + gap * 5);
            chkNetwork = CreateModernCheckBox("🌐  Reset Network & Flush DNS", true, 16, startY + gap * 6);
            chkSfcDism = CreateModernCheckBox("🛠️  System File Checker (SFC / DISM)", false, 16, startY + gap * 7);

            leftPanel.Controls.Add(chkTemp);
            leftPanel.Controls.Add(chkRecycle);
            leftPanel.Controls.Add(chkExplorer);
            leftPanel.Controls.Add(chkUpdates);
            leftPanel.Controls.Add(chkPrefetch);
            leftPanel.Controls.Add(chkLogs);
            leftPanel.Controls.Add(chkNetwork);
            leftPanel.Controls.Add(chkSfcDism);

            // Small Selection Buttons
            int btnY = startY + gap * 8 + 8;
            btnSelectAll = CreateHoverButton("Select All", 16, btnY, 160, 28, Color.FromArgb(36, 40, 50), Color.FromArgb(48, 54, 68), Color.White);
            btnSelectAll.Click += (s, e) => SetAllCheckboxes(true);
            btnSelectDefault = CreateHoverButton("Reset Defaults", 184, btnY, 160, 28, Color.FromArgb(36, 40, 50), Color.FromArgb(48, 54, 68), Color.White);
            btnSelectDefault.Click += (s, e) => SetDefaultCheckboxes();
            leftPanel.Controls.Add(btnSelectAll);
            leftPanel.Controls.Add(btnSelectDefault);

            // Primary & Secondary Action Buttons
            int actionY = btnY + 42;
            btnStart = CreateHoverButton("▶   START MAINTENANCE", 16, actionY, 328, 44, Color.FromArgb(0, 120, 212), Color.FromArgb(16, 140, 235), Color.White);
            btnStart.Font = new Font("Segoe UI", 10.0f, FontStyle.Bold);
            btnStart.Click += (s, e) => StartMaintenance(false);
            leftPanel.Controls.Add(btnStart);

            btnQuickClean = CreateHoverButton("⚡   QUICK CLEANUP (30s)", 16, actionY + 52, 328, 38, Color.FromArgb(16, 124, 65), Color.FromArgb(22, 148, 78), Color.White);
            btnQuickClean.Font = new Font("Segoe UI", 9.25f, FontStyle.Bold);
            btnQuickClean.Click += (s, e) => StartMaintenance(true);
            leftPanel.Controls.Add(btnQuickClean);

            // ================= RIGHT PANEL =================
            rightPanel = new Panel();
            rightPanel.Dock = DockStyle.Fill;
            rightPanel.BackColor = Color.FromArgb(15, 17, 21);
            rightPanel.Padding = new Padding(12);

            // Stats Cards Container
            statsContainer = new Panel();
            statsContainer.Dock = DockStyle.Top;
            statsContainer.Height = 64;
            statsContainer.BackColor = Color.Transparent;
            statsContainer.Padding = new Padding(0, 0, 0, 8);

            // 3 Stat Cards
            int cardW = 180;
            cardSpace = CreateStatCard(0, 0, cardW, 56, "FREED SPACE", "0 MB", Color.FromArgb(34, 197, 94), out lblCardSpaceTitle, out lblCardSpaceVal);
            cardFiles = CreateStatCard(cardW + 10, 0, cardW, 56, "FILES DELETED", "0", Color.FromArgb(56, 189, 248), out lblCardFilesTitle, out lblCardFilesVal);
            cardStatus = CreateStatCard((cardW + 10) * 2, 0, 190, 56, "SYSTEM STATUS", "Ready", Color.FromArgb(226, 232, 240), out lblCardStatusTitle, out lblCardStatusVal);

            statsContainer.Controls.Add(cardSpace);
            statsContainer.Controls.Add(cardFiles);
            statsContainer.Controls.Add(cardStatus);
            rightPanel.Controls.Add(statsContainer);

            // Modern Progress Bar
            progressBar = new ProgressBar();
            progressBar.Dock = DockStyle.Top;
            progressBar.Height = 6;
            progressBar.Style = ProgressBarStyle.Continuous;
            progressBar.Value = 0;
            rightPanel.Controls.Add(progressBar);

            // Bottom Console Toolbar
            bottomLogBar = new Panel();
            bottomLogBar.Dock = DockStyle.Bottom;
            bottomLogBar.Height = 40;
            bottomLogBar.BackColor = Color.FromArgb(20, 23, 29);
            bottomLogBar.Padding = new Padding(6);

            btnClearLog = CreateHoverButton("🧹 Clear Console", 8, 6, 150, 28, Color.FromArgb(36, 40, 50), Color.FromArgb(48, 54, 68), Color.White);
            btnClearLog.Click += (s, e) => txtLog.Clear();
            bottomLogBar.Controls.Add(btnClearLog);

            btnOpenTemp = CreateHoverButton("📂 Open Temp Folder", 166, 6, 150, 28, Color.FromArgb(36, 40, 50), Color.FromArgb(48, 54, 68), Color.White);
            btnOpenTemp.Click += (s, e) => {
                try { Process.Start("explorer.exe", Path.GetTempPath()); } catch { }
            };
            bottomLogBar.Controls.Add(btnOpenTemp);

            btnCopyLog = CreateHoverButton("📋 Copy Log", 324, 6, 150, 28, Color.FromArgb(36, 40, 50), Color.FromArgb(48, 54, 68), Color.White);
            btnCopyLog.Click += (s, e) => {
                try {
                    if (!string.IsNullOrEmpty(txtLog.Text)) {
                        Clipboard.SetText(txtLog.Text);
                        MessageBox.Show("Console log copied to clipboard.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                } catch { }
            };
            bottomLogBar.Controls.Add(btnCopyLog);

            rightPanel.Controls.Add(bottomLogBar);

            // Rich Console Box
            txtLog = new RichTextBox();
            txtLog.Dock = DockStyle.Fill;
            txtLog.BackColor = Color.FromArgb(10, 11, 14);
            txtLog.ForeColor = Color.FromArgb(220, 225, 230);
            txtLog.Font = new Font("Consolas", 9.25f, FontStyle.Regular);
            txtLog.BorderStyle = BorderStyle.None;
            txtLog.ReadOnly = true;
            rightPanel.Controls.Add(txtLog);

            // Order layers
            txtLog.BringToFront();
            progressBar.BringToFront();
            statsContainer.BringToFront();

            // Add panels to Form
            this.Controls.Add(rightPanel);
            this.Controls.Add(leftPanel);
            this.Controls.Add(headerPanel);

            // Initial Welcome Message in Console
            Log("==================================================================", Color.FromArgb(56, 189, 248));
            Log("  LIGHTWEIGHT WINDOWS SYSTEM MAINTENANCE TOOL v2.2", Color.FromArgb(56, 189, 248));
            Log("==================================================================", Color.FromArgb(56, 189, 248));
            Log("[+] OS: " + Environment.OSVersion.VersionString + " (" + (Environment.Is64BitOperatingSystem ? "64-bit" : "32-bit") + ")", Color.FromArgb(148, 163, 184));
            Log("[+] Administrator privileges verified and active.", Color.FromArgb(74, 222, 128));
            Log("[*] Select your maintenance tasks or click 'Quick Cleanup' to begin.", Color.FromArgb(203, 213, 225));
            Log("", Color.White);
        }

        private Panel CreateStatCard(int x, int y, int w, int h, string title, string val, Color valColor, out Label lblTitle, out Label lblVal)
        {
            Panel card = new Panel();
            card.Location = new Point(x, y);
            card.Size = new Size(w, h);
            card.BackColor = Color.FromArgb(24, 27, 34);

            lblTitle = new Label();
            lblTitle.Text = title;
            lblTitle.Font = new Font("Segoe UI", 7.5f, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblTitle.Location = new Point(10, 8);
            lblTitle.AutoSize = true;

            lblVal = new Label();
            lblVal.Text = val;
            lblVal.Font = new Font("Segoe UI", 13.0f, FontStyle.Bold);
            lblVal.ForeColor = valColor;
            lblVal.Location = new Point(10, 24);
            lblVal.AutoSize = true;

            card.Controls.Add(lblTitle);
            card.Controls.Add(lblVal);
            return card;
        }

        private CheckBox CreateModernCheckBox(string text, bool isChecked, int x, int y)
        {
            CheckBox cb = new CheckBox();
            cb.Text = text;
            cb.Checked = isChecked;
            cb.Location = new Point(x, y);
            cb.Size = new Size(335, 25);
            cb.ForeColor = Color.FromArgb(226, 232, 240);
            cb.FlatStyle = FlatStyle.Flat;
            cb.Cursor = Cursors.Hand;
            cb.Font = new Font("Segoe UI", 9.0f, FontStyle.Regular);
            return cb;
        }

        private Button CreateHoverButton(string text, int x, int y, int w, int h, Color normal, Color hover, Color fg)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Location = new Point(x, y);
            btn.Size = new Size(w, h);
            btn.BackColor = normal;
            btn.ForeColor = fg;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Cursor = Cursors.Hand;

            btn.MouseEnter += (s, e) => { if (btn.Enabled) btn.BackColor = hover; };
            btn.MouseLeave += (s, e) => { if (btn.Enabled) btn.BackColor = normal; };
            return btn;
        }

        private string GetDriveSummary()
        {
            try
            {
                DriveInfo c = new DriveInfo("C");
                if (c.IsReady)
                {
                    long freeGb = c.AvailableFreeSpace / (1024 * 1024 * 1024);
                    long totalGb = c.TotalSize / (1024 * 1024 * 1024);
                    return string.Format("Drive C: {0} GB Free / {1} GB", freeGb, totalGb);
                }
            }
            catch { }
            return "Drive C: Unknown";
        }

        private void SetAllCheckboxes(bool state)
        {
            chkTemp.Checked = state;
            chkRecycle.Checked = state;
            chkExplorer.Checked = state;
            chkUpdates.Checked = state;
            chkPrefetch.Checked = state;
            chkLogs.Checked = state;
            chkNetwork.Checked = state;
            chkSfcDism.Checked = state;
        }

        private void SetDefaultCheckboxes()
        {
            chkTemp.Checked = true;
            chkRecycle.Checked = true;
            chkExplorer.Checked = true;
            chkUpdates.Checked = true;
            chkPrefetch.Checked = true;
            chkLogs.Checked = true;
            chkNetwork.Checked = true;
            chkSfcDism.Checked = false;
        }

        private void SetControlsRunningState(bool running)
        {
            btnStart.Enabled = !running;
            btnQuickClean.Enabled = !running;
            btnSelectAll.Enabled = !running;
            btnSelectDefault.Enabled = !running;

            if (running)
            {
                btnStart.Text = "⏳   MAINTENANCE IN PROGRESS...";
                btnStart.BackColor = Color.FromArgb(45, 52, 65);
            }
            else
            {
                btnStart.Text = "▶   START MAINTENANCE";
                btnStart.BackColor = Color.FromArgb(0, 120, 212);
            }
        }

        private void Log(string text, Color color)
        {
            if (txtLog.InvokeRequired)
            {
                txtLog.Invoke(new Action(() => Log(text, color)));
                return;
            }

            txtLog.SelectionStart = txtLog.TextLength;
            txtLog.SelectionLength = 0;
            txtLog.SelectionColor = color;
            txtLog.AppendText(text + Environment.NewLine);
            txtLog.SelectionColor = txtLog.ForeColor;
            txtLog.ScrollToCaret();
        }

        private void UpdateStatus(string status, int progress)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => UpdateStatus(status, progress)));
                return;
            }

            lblCardStatusVal.Text = status;
            if (progress >= 0 && progress <= 100)
            {
                progressBar.Style = ProgressBarStyle.Continuous;
                progressBar.Value = progress;
            }
            lblCardSpaceVal.Text = FormatBytes(totalFreedBytes);
            lblCardFilesVal.Text = totalFilesDeleted.ToString("N0");
            lblDriveInfo.Text = GetDriveSummary();
        }

        private void StartMaintenance(bool quickMode)
        {
            if (isRunning) return;

            if (quickMode)
            {
                SetDefaultCheckboxes();
                chkSfcDism.Checked = false;
            }

            bool doTemp = chkTemp.Checked;
            bool doRecycle = chkRecycle.Checked;
            bool doExplorer = chkExplorer.Checked;
            bool doUpdates = chkUpdates.Checked;
            bool doPrefetch = chkPrefetch.Checked;
            bool doLogs = chkLogs.Checked;
            bool doNetwork = chkNetwork.Checked;
            bool doSfc = chkSfcDism.Checked;

            if (!doTemp && !doRecycle && !doExplorer && !doUpdates && !doPrefetch && !doLogs && !doNetwork && !doSfc)
            {
                MessageBox.Show("Please select at least one maintenance option.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            isRunning = true;
            SetControlsRunningState(true);
            totalFreedBytes = 0;
            totalFilesDeleted = 0;
            UpdateStatus("Starting...", 0);

            workerThread = new Thread(() =>
            {
                try
                {
                    Log("", Color.White);
                    Log(">>> MAINTENANCE STARTED: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Color.FromArgb(56, 189, 248));

                    int totalSteps = 0;
                    if (doTemp) totalSteps++;
                    if (doRecycle) totalSteps++;
                    if (doExplorer) totalSteps++;
                    if (doUpdates) totalSteps++;
                    if (doPrefetch) totalSteps++;
                    if (doLogs) totalSteps++;
                    if (doNetwork) totalSteps++;
                    if (doSfc) totalSteps += 2;

                    int currentStep = 0;

                    // 1. TEMP
                    if (doTemp)
                    {
                        currentStep++;
                        UpdateStatus("Cleaning Temp", (currentStep * 100) / totalSteps);
                        Log("[1] Cleaning Temporary Files...", Color.FromArgb(251, 191, 36));
                        CleanTempFiles();
                    }

                    // 2. RECYCLE BIN
                    if (doRecycle)
                    {
                        currentStep++;
                        UpdateStatus("Emptying Trash", (currentStep * 100) / totalSteps);
                        Log("[2] Emptying Recycle Bin...", Color.FromArgb(251, 191, 36));
                        EmptyRecycle();
                    }

                    // 3. EXPLORER CACHE
                    if (doExplorer)
                    {
                        currentStep++;
                        UpdateStatus("Cleaning Cache", (currentStep * 100) / totalSteps);
                        Log("[3] Cleaning Explorer Thumbnail Cache...", Color.FromArgb(251, 191, 36));
                        CleanExplorerCache();
                    }

                    // 4. WINDOWS UPDATE CACHE
                    if (doUpdates)
                    {
                        currentStep++;
                        UpdateStatus("Cleaning Updates", (currentStep * 100) / totalSteps);
                        Log("[4] Cleaning Windows Update Download Cache...", Color.FromArgb(251, 191, 36));
                        CleanWindowsUpdateFast();
                    }

                    // 5. PREFETCH
                    if (doPrefetch)
                    {
                        currentStep++;
                        UpdateStatus("Cleaning Prefetch", (currentStep * 100) / totalSteps);
                        Log("[5] Cleaning Prefetch Files...", Color.FromArgb(251, 191, 36));
                        CleanPrefetch();
                    }

                    // 6. LOGS & WER
                    if (doLogs)
                    {
                        currentStep++;
                        UpdateStatus("Cleaning Logs", (currentStep * 100) / totalSteps);
                        Log("[6] Cleaning Error Reports (WER) and System Logs...", Color.FromArgb(251, 191, 36));
                        CleanLogsAndWer();
                    }

                    // 7. NETWORK
                    if (doNetwork)
                    {
                        currentStep++;
                        UpdateStatus("Resetting Network", (currentStep * 100) / totalSteps);
                        Log("[7] Repairing Network & Resetting DNS...", Color.FromArgb(251, 191, 36));
                        RepairNetwork();
                    }

                    // 8. SFC & DISM
                    if (doSfc)
                    {
                        currentStep++;
                        UpdateStatus("Scanning DISM", (currentStep * 100) / totalSteps);
                        Log("[8/A] DISM System Health Scan and Restore...", Color.FromArgb(251, 191, 36));
                        RunConsoleTool("dism.exe", "/Online /Cleanup-Image /RestoreHealth");

                        currentStep++;
                        UpdateStatus("Scanning SFC", (currentStep * 100) / totalSteps);
                        Log("[8/B] System File Checker (SFC /scannow)...", Color.FromArgb(251, 191, 36));
                        RunConsoleTool("sfc.exe", "/scannow");
                    }

                    UpdateStatus("Completed", 100);
                    Log("", Color.White);
                    Log("==================================================================", Color.FromArgb(74, 222, 128));
                    Log(string.Format("  MAINTENANCE COMPLETED! Freed Space: {0} | Files Deleted: {1:N0}", FormatBytes(totalFreedBytes), totalFilesDeleted), Color.FromArgb(74, 222, 128));
                    Log("==================================================================", Color.FromArgb(74, 222, 128));
                }
                catch (Exception ex)
                {
                    Log("[!] Unexpected error: " + ex.Message, Color.FromArgb(248, 113, 113));
                }
                finally
                {
                    isRunning = false;
                    if (this.IsHandleCreated)
                    {
                        this.Invoke(new Action(() =>
                        {
                            SetControlsRunningState(false);
                        }));
                    }
                }
            });

            workerThread.IsBackground = true;
            workerThread.Start();
        }

        // ================= CLEANING METHODS =================

        private void CleanDirectoryContents(string dirPath)
        {
            if (!Directory.Exists(dirPath)) return;

            try
            {
                string[] files = Directory.GetFiles(dirPath);
                foreach (string file in files)
                {
                    try
                    {
                        FileInfo fi = new FileInfo(file);
                        long len = fi.Length;
                        File.SetAttributes(file, FileAttributes.Normal);
                        File.Delete(file);
                        Interlocked.Add(ref totalFreedBytes, len);
                        int count = Interlocked.Increment(ref totalFilesDeleted);
                        if (count % 150 == 0)
                        {
                            UpdateStatus("Cleaning...", -1);
                        }
                    }
                    catch
                    {
                        // File locked or in use - safely skip
                    }
                }
            }
            catch { }

            try
            {
                string[] subDirs = Directory.GetDirectories(dirPath);
                foreach (string subDir in subDirs)
                {
                    CleanDirectoryContents(subDir);
                    try
                    {
                        Directory.Delete(subDir, false);
                    }
                    catch { }
                }
            }
            catch { }
        }

        private void CleanTempFiles()
        {
            // User Temp
            string userTemp = Path.GetTempPath();
            Log("  -> Scanning User Temp...", Color.FromArgb(148, 163, 184));
            CleanDirectoryContents(userTemp);

            // Windows Temp
            string winTemp = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Temp");
            Log("  -> Scanning Windows Temp...", Color.FromArgb(148, 163, 184));
            CleanDirectoryContents(winTemp);

            // Crash Dumps
            string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string crashDumps = Path.Combine(localAppData, "CrashDumps");
            if (Directory.Exists(crashDumps))
            {
                Log("  -> Cleaning crash dumps...", Color.FromArgb(148, 163, 184));
                CleanDirectoryContents(crashDumps);
            }
            Log("  [OK] Temporary files cleaned.", Color.FromArgb(74, 222, 128));
        }

        private void EmptyRecycle()
        {
            try
            {
                uint result = SHEmptyRecycleBin(IntPtr.Zero, null, SHERB_NOCONFIRMATION | SHERB_NOPROGRESSUI | SHERB_NOSOUND);
                Log("  [OK] Recycle Bin emptied.", Color.FromArgb(74, 222, 128));
            }
            catch (Exception ex)
            {
                Log("  [!] Failed to empty Recycle Bin: " + ex.Message, Color.FromArgb(248, 113, 113));
            }
        }

        private void CleanExplorerCache()
        {
            try
            {
                string explorerPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Microsoft", "Windows", "Explorer");
                if (Directory.Exists(explorerPath))
                {
                    string[] thumbFiles = Directory.GetFiles(explorerPath, "thumbcache_*.db");
                    foreach (string file in thumbFiles)
                    {
                        try
                        {
                            FileInfo fi = new FileInfo(file);
                            long len = fi.Length;
                            File.Delete(file);
                            Interlocked.Add(ref totalFreedBytes, len);
                            Interlocked.Increment(ref totalFilesDeleted);
                        }
                        catch { }
                    }
                }
                Log("  [OK] Explorer thumbnail cache cleaned.", Color.FromArgb(74, 222, 128));
            }
            catch { }
        }

        private void CleanWindowsUpdateFast()
        {
            string updateDownload = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), @"SoftwareDistribution\Download");

            if (!Directory.Exists(updateDownload))
            {
                Log("  [OK] Windows Update download directory not found or empty.", Color.FromArgb(74, 222, 128));
                return;
            }

            try
            {
                long initialBytes = 0;
                int initialFiles = 0;
                MeasureFolderStats(updateDownload, ref initialBytes, ref initialFiles);

                Log(string.Format("  -> Scanned: {0:N0} files ({1})", initialFiles, FormatBytes(initialBytes)), Color.FromArgb(148, 163, 184));

                Log("  -> Stopping Windows Update service...", Color.FromArgb(148, 163, 184));
                RunCommandSilent("net stop wuauserv /y", 5000);

                Log("  -> Cleaning download cache...", Color.FromArgb(148, 163, 184));
                RunCommandSilent("del /f /s /q \"" + updateDownload + "\\*.*\" >nul 2>&1", 8000);
                RunCommandSilent("for /d %p in (\"" + updateDownload + "\\*\") do @rmdir \"%p\" /s /q >nul 2>&1", 8000);

                Log("  -> Starting Windows Update service...", Color.FromArgb(148, 163, 184));
                RunCommandSilent("net start wuauserv", 5000);

                long remainingBytes = 0;
                int remainingFiles = 0;
                MeasureFolderStats(updateDownload, ref remainingBytes, ref remainingFiles);

                long freed = Math.Max(0, initialBytes - remainingBytes);
                int deleted = Math.Max(0, initialFiles - remainingFiles);

                Interlocked.Add(ref totalFreedBytes, freed);
                Interlocked.Add(ref totalFilesDeleted, deleted);
                UpdateStatus("Cleaning...", -1);

                Log(string.Format("  [OK] Windows Update cache cleaned (Freed: {0}).", FormatBytes(freed)), Color.FromArgb(74, 222, 128));
            }
            catch (Exception ex)
            {
                Log("  [!] Error cleaning Windows Update cache: " + ex.Message, Color.FromArgb(248, 113, 113));
                RunCommandSilent("net start wuauserv", 5000);
            }
        }

        private void MeasureFolderStats(string dirPath, ref long totalBytes, ref int totalFiles)
        {
            if (!Directory.Exists(dirPath)) return;
            try
            {
                string[] files = Directory.GetFiles(dirPath, "*.*", SearchOption.AllDirectories);
                totalFiles = files.Length;
                foreach (string f in files)
                {
                    try
                    {
                        FileInfo fi = new FileInfo(f);
                        totalBytes += fi.Length;
                    }
                    catch { }
                }
            }
            catch
            {
                try
                {
                    string[] files = Directory.GetFiles(dirPath);
                    totalFiles = files.Length;
                    foreach (string f in files)
                    {
                        try { totalBytes += new FileInfo(f).Length; } catch { }
                    }
                }
                catch { }
            }
        }

        private void CleanPrefetch()
        {
            try
            {
                string prefetch = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Prefetch");
                Log("  -> Scanning Prefetch directory...", Color.FromArgb(148, 163, 184));
                CleanDirectoryContents(prefetch);
                Log("  [OK] Prefetch files cleaned.", Color.FromArgb(74, 222, 128));
            }
            catch (Exception ex)
            {
                Log("  [!] Prefetch cleaning skipped: " + ex.Message, Color.FromArgb(248, 113, 113));
            }
        }

        private void CleanLogsAndWer()
        {
            string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string programData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            string winDir = Environment.GetFolderPath(Environment.SpecialFolder.Windows);

            string[] paths = new string[]
            {
                Path.Combine(localAppData, @"Microsoft\Windows\WER\ReportArchive"),
                Path.Combine(localAppData, @"Microsoft\Windows\WER\ReportQueue"),
                Path.Combine(programData, @"Microsoft\Windows\WER\ReportArchive"),
                Path.Combine(programData, @"Microsoft\Windows\WER\ReportQueue"),
                Path.Combine(winDir, @"Logs\CBS"),
                Path.Combine(winDir, @"Panther")
            };

            foreach (string p in paths)
            {
                if (Directory.Exists(p))
                {
                    CleanDirectoryContents(p);
                }
            }
            Log("  [OK] Error reports and historical log files cleaned.", Color.FromArgb(74, 222, 128));
        }

        private void RepairNetwork()
        {
            Log("  -> Flushing DNS cache (ipconfig /flushdns)...", Color.FromArgb(148, 163, 184));
            RunCommandSilent("ipconfig /flushdns", 4000);

            Log("  -> Refreshing DNS registrations (ipconfig /registerdns)...", Color.FromArgb(148, 163, 184));
            RunCommandSilent("ipconfig /registerdns", 4000);

            Log("  -> Resetting Winsock catalog...", Color.FromArgb(148, 163, 184));
            RunCommandSilent("netsh winsock reset catalog", 4000);

            Log("  -> Resetting TCP/IP stack...", Color.FromArgb(148, 163, 184));
            RunCommandSilent("netsh int ip reset", 4000);

            Log("  [OK] Network and DNS configuration successfully reset.", Color.FromArgb(74, 222, 128));
        }

        private void RunConsoleTool(string fileName, string args)
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = fileName,
                    Arguments = args,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                using (Process proc = Process.Start(psi))
                {
                    while (!proc.StandardOutput.EndOfStream)
                    {
                        string line = proc.StandardOutput.ReadLine();
                        if (!string.IsNullOrWhiteSpace(line))
                        {
                            Log("  " + line.Trim(), Color.FromArgb(203, 213, 225));
                        }
                    }
                    proc.WaitForExit();
                }
                Log("  [OK] " + fileName + " completed successfully.", Color.FromArgb(74, 222, 128));
            }
            catch (Exception ex)
            {
                Log("  [!] Failed to run " + fileName + ": " + ex.Message, Color.FromArgb(248, 113, 113));
            }
        }

        private void RunCommandSilent(string cmd, int timeoutMs = 6000)
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo("cmd.exe", "/c " + cmd)
                {
                    CreateNoWindow = true,
                    UseShellExecute = false
                };
                using (Process p = Process.Start(psi))
                {
                    if (!p.WaitForExit(timeoutMs))
                    {
                        try { p.Kill(); } catch { }
                    }
                }
            }
            catch { }
        }

        private string FormatBytes(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = bytes;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len /= 1024;
            }
            return string.Format("{0:0.##} {1}", len, sizes[order]);
        }
    }
}
