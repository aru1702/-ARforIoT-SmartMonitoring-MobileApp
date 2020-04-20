using System;

namespace Models
{
	[Serializable]
	public class GetDeviceDetailsModel2
	{
		public int code;
        public string msg;
        public bool success;
        public DeviceDetail2 result;

		public override string ToString(){
			return UnityEngine.JsonUtility.ToJson (this, true);
		}
	}

    [Serializable]
    public class DeviceDetail2
    {
        public string id;
		public string name;
		public string description;
		public string id_user;
		public string last_update;
    }
}

