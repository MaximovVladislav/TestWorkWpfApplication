namespace TestWork.PL.Infrastructure.Interfaces
{
    public interface IClosableDialog
    {
        void Close();
        bool? DialogResult { get; set; }
    }
}