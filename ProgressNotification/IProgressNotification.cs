namespace DevAzt.FormsX.UI.Notifications
{
    public interface IProgressNotification
    {
        /// <summary>
        /// Implement on set method, SendNotification()
        /// </summary>
        double Progress { get; set; }

        /// <summary>
        /// Total of progress
        /// </summary>
        double Total { get; set; }

        //Permite mostrar una notificacion con un progressindicator
        void Show(string title, string messageprogress, string messagecomplete);

        /// <summary>
        /// Ends Notification
        /// </summary>
        void Complete();
    }
}
