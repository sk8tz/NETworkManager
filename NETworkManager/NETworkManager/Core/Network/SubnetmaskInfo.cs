namespace NETworkManager.Core.Network
{
    public class SubnetmaskInfo
    {
        public int Suffix { get; set; }
        public string Subnetmask { get; set; }
        public int Hosts { get; set; }        

        public SubnetmaskInfo()
        {

        }

        public SubnetmaskInfo(int suffix, string subnetmask, int hosts)
        {
            Suffix = suffix;
            Subnetmask = subnetmask;
            Hosts = hosts;
        }

        public override string ToString()
        {
            return string.Format("{0} (/{1})", Subnetmask, Suffix);
        }
    }
}
