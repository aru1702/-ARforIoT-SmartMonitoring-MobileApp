using System;

namespace Models
{
	[Serializable]
	public class GetUserIdModel
	{
		public int code;
        public string msg;
        public bool success;
        public UserId result;

		public override string ToString(){
			return UnityEngine.JsonUtility.ToJson (this, true);
		}
	}

    [Serializable]
    public class UserId
    {
        public string id;
    }
}

