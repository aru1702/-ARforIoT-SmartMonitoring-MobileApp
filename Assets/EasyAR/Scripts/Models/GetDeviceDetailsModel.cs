using System;

namespace Models
{
	[Serializable]
	public class GetDeviceDetailsModel
	{
		public int code;
        public string msg;
        public bool success;
        public DeviceDetail result;

		public override string ToString(){
			return UnityEngine.JsonUtility.ToJson (this, true);
		}
	}

    [Serializable]
    public class DeviceDetail
    {
        public string id;
		public string name;
		public string description;
		public string id_user;
		public string last_update;
    }
}

