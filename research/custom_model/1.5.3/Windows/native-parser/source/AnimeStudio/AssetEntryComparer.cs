using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeStudio
{
    public class AssetEntryComparer : IEqualityComparer<AssetEntry>
    {
        public bool Equals(AssetEntry x, AssetEntry y)
        {
            if (x == null || y == null) return false;
            return x.Name == y.Name && x.Container == y.Container && x.PathID == y.PathID;
        }

        public int GetHashCode(AssetEntry obj)
        {
            if (obj == null) return 0;
            int hashName = obj.Name?.GetHashCode() ?? 0;
            int hashContainer = obj.Container?.GetHashCode() ?? 0;
            int hashPathID = obj.PathID.GetHashCode();
            return hashName ^ hashContainer ^ hashPathID;
        }
    }
}
