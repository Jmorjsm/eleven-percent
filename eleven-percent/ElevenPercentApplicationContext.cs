namespace eleven_percent;

public class ElevenPercentApplicationContext : ApplicationContext
{
    private NotifyIcon _trayIcon;

    public ElevenPercentApplicationContext()
    {
        ContextMenuStrip contextMenuStrip = new ContextMenuStrip()
        {
        };

        contextMenuStrip.Items.Add(new ToolStripMenuItem("Exit", null, Exit));
        
        _trayIcon = new NotifyIcon()
        {
            Icon = IconFromText("11"),
            ContextMenuStrip = contextMenuStrip,
            Visible = true,
        };
        
    }

    private static Icon IconFromText(string str)
    {
        Font fontToUse = new Font("Microsoft Sans Serif", 16, FontStyle.Regular, GraphicsUnit.Pixel);
        Brush brushToUse = new SolidBrush(Color.White);
        Bitmap bitmapText = new Bitmap(16, 16);
        Graphics g = Graphics.FromImage(bitmapText);

        IntPtr hIcon;

        g.Clear(Color.Transparent);
        g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
        g.DrawString(str, fontToUse, brushToUse, -4, -2);
        hIcon = (bitmapText.GetHicon());
        Icon fromHandle = System.Drawing.Icon.FromHandle(hIcon);
        return fromHandle;
        //DestroyIcon(hIcon.ToInt32);
    }

    void Exit(object? sender, EventArgs e)
    {
        _trayIcon.Visible = false;
        Application.Exit();
    }
}