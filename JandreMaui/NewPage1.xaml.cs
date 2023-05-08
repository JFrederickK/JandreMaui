using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace JandreMaui;

public partial class NewPage1 : ContentPage
{
    public NewPage1()
    {
        InitializeComponent();

    }



}

public class YoMamaCommand : ICommand
{
    private Func<object, bool> canExecute;
    private Action<object> action;

    public YoMamaCommand(
        Func<object, bool> canExecute,
        Action<object> action
        )
    {
        this.canExecute = canExecute;
        this.action = action;
    }

    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter)
    {
        return this.canExecute(parameter);
    }

    public void Execute(object parameter)
    {
        this.action(parameter);
    }

    public void NotifyCanExecuteChanged()
    {
        if (this.CanExecuteChanged != null)
        {
            this.CanExecuteChanged.Invoke(this, EventArgs.Empty);
        }
    }


}

public class YoMamaViewModel : INotifyPropertyChanged
{
    public YoMamaViewModel()
    {
        this.Number = 10;
        this.ButtonCommand = new YoMamaCommand(this.CanButton1Execute, this.Button1Action);
        this.Button2Command = new YoMamaCommand(this.CanButton2Execute, this.Button2Action);


    }



    public event PropertyChangedEventHandler PropertyChanged;

    private bool CanButton2Execute(object arg)
    {
        return true;
    }

    private void Button2Action(object obj)
    {

    }

    private void Button1Action(object obj)
    {
        this.Number++;

    }

    private bool CanButton1Execute(object arg)
    {
        return this.Number < 20;
    }

    public YoMamaCommand ButtonCommand { get; set; }

    public YoMamaCommand Button2Command { get; set; }


    public int Number
    {
        get
        {
            return this.number;
        }
        set
        {
            if (this.number != value)
            {
                this.number = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(Number)));
                    this.ButtonCommand.NotifyCanExecuteChanged();
                }
            }
        }
    }


    private int number;


}
