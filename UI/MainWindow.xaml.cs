using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Autofac;
using BusinessLogicLeve;
using Projects.Entities;

namespace UI;
public partial class MainWindow : Window
{
    private readonly IContainer _container;
    public IEnumerable<Account> Accounts { get; } = new List<Account>();

    public MainWindow()
    {
        InitializeComponent();
        DataContext = this;
        this._container = ContainerConfig.Configure();
        using (var scope = _container.BeginLifetimeScope())
        {
            var bankService = scope.Resolve<BankService>();
            this.Accounts = new List<Account>(bankService.GetAllWallets());
        }
    }
}
