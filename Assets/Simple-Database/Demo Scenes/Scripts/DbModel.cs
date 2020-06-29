using System;

namespace Scripts
{
    [Serializable]
	public class DbModel
	{
		public string key;
        public string value;

		public override string ToString(){
			return UnityEngine.JsonUtility.ToJson (this, true);
		}
	}
}
