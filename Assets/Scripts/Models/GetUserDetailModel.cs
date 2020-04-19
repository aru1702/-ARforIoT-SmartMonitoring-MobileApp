using System;

namespace Models
{
	[Serializable]
	public class GetUserDetailModel
	{
		public int code;
        public string msg;
        public bool success;
        public UserDetail result;

		public override string ToString(){
			return UnityEngine.JsonUtility.ToJson (this, true);
		}
	}

    [Serializable]
    public class UserDetail
    {
        public string id;
        public string name;
        public string email;
        public string last_update;
    }
}

