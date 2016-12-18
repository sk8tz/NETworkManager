namespace NETworkManager.Core.Network
{
    public class SubnetmaskInfo
    {
        public int Suffix { get; set; }
        public string Subnetmask { get; set; }

        public SubnetmaskInfo()
        {

        }

        public SubnetmaskInfo(int suffix, string subnetmask)
        {
            Suffix = suffix;
            Subnetmask = subnetmask;
        }

        public override string ToString()
        {
            return string.Format("{0} (/{1})", Subnetmask, Suffix);
        }
    }
}
