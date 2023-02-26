using Timer = System.Windows.Forms.Timer;

namespace eleven_percent;

public class ElevenPercentApplicationContext : ApplicationContext
{
    private NotifyIcon _trayIcon;
    private Timer _timer;
    public ElevenPercentApplicationContext()
    {
        ContextMenuStrip contextMenuStrip = new ContextMenuStrip();

        contextMenuStrip.Items.Add(new ToolStripMenuItem("Exit", null, Exit));
        int percent = GetBatteryPercent();
        _trayIcon = new NotifyIcon()
        {
            Icon = IconFromText(percent.ToString()),
            ContextMenuStrip = contextMenuStrip,
            Visible = true,
        };
        this._timer = new Timer()
        {
            Interval = 5000,
        };
        
        _timer.Tick += UpdateBatteryPercent;

        _timer.Start();
    }

    private void UpdateBatteryPercent(object? sender, EventArgs args)
    {
        Icon? oldIcon = _trayIcon.Icon;
        int percent = GetBatteryPercent();
        _trayIcon.Icon = IconFromText(percent.ToString());
        oldIcon?.Dispose();
    }

    private static int GetBatteryPercent()
    {
        PowerStatus powerStatus = SystemInformation.PowerStatus;
        int percent = (int)Math.Ceiling(powerStatus.BatteryLifePercent * 100d);
        return percent;
    }

    private static Icon IconFromText(string str)
    {
        Font fontToUse = new Font("Microsoft Sans Serif", 10, FontStyle.Regular, GraphicsUnit.Pixel);
        Brush brushToUse = new SolidBrush(Color.Black);
        Bitmap bitmapText = new Bitmap(16, 16);
        Graphics g = Graphics.FromImage(bitmapText);

        nint hIcon;

        g.Clear(Color.Transparent);
        g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
        float xOffset;
        if (str.Length == 3)
        {
            xOffset = -4;
        }
        else if (str.Length == 2)
        {
            xOffset = 0;
        }
        else
        {
            xOffset = 4;
        }
        
        g.DrawString(str, fontToUse, brushToUse, xOffset, 0);
        hIcon = (bitmapText.GetHicon());
        Icon fromHandle = Icon.FromHandle(hIcon);
        return fromHandle;
        //DestroyIcon(hIcon.ToInt32);
    }

    void Exit(object? sender, EventArgs e)
    {
        _trayIcon.Visible = false;
        this.Dispose();
        Application.Exit();
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        this._timer.Dispose();
        this._trayIcon.Dispose();
    }
}