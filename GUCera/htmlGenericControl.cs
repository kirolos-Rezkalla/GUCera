namespace GUCera
{
    internal class htmlGenericControl
    {
        private string v;

        public htmlGenericControl(string v)
        {
            this.v = v;
        }

        public string InnerText { get; internal set; }
        public object Controls { get; internal set; }
    }
}