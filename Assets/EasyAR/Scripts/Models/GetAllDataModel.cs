using System;

namespace Models
{
	[Serializable]
	public class GetAllDataModel
	{
		public int code;
        public string msg;
        public bool success;
        public DataList[] result;

		public override string ToString(){
			return UnityEngine.JsonUtility.ToJson (this, true);
		}
	}

    [Serializable]
    public class DataList
    {
        public string id;
		public string name;
		public string value;
		public string id_device;
		public string last_update;
    }
}

