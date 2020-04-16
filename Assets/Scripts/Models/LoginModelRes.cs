using System;

namespace Models
{
	[Serializable]
	public class LoginModelRes
	{
		public int code;

		public string msg;

        public bool success;

        public string result;
		
		public override string ToString(){
			return UnityEngine.JsonUtility.ToJson (this, true);
		}
	}
}

