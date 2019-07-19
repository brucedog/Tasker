namespace InterfaceLibrary.Interface
{
    public interface IAlertTimeViewModel
    {
        int Hour { get; set; }

        int Minute { get; set; }

        string AmOrPm { get; set; }
    }
}