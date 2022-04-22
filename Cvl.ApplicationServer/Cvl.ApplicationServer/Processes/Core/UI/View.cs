namespace Cvl.ApplicationServer.Core.Processes.UI
{
    public class View
    {
        public View()
        {
            ViewName = string.Empty;
        }
        public View(string viewName)
        {
            ViewName = viewName;
        }
        public View(string viewName, object viewModel, params string[] actionsName)
        {
            ViewName = viewName;
            ViewModel = viewModel;
            foreach (var actionName in actionsName)
            {
                Actions.Add(new UIAction(){ActionName = actionName});
            }
        }
        public string ViewName { get; set; }
        public string? ViewDescription { get; set; }
        public object? ViewModel { get; set; }

        public List<UIAction> Actions { get; set; } = new List<UIAction>();
    }
}
