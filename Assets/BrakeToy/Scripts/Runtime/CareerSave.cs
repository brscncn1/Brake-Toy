using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using BrakeToy.Core;
using UnityEngine;
namespace BrakeToy
{
    public sealed class CareerSave
    {
        [Serializable] private sealed class Envelope{public int version=1;public string payload,checksum;}
        public string PathName{get;}public bool Recovered{get;private set;}
        public CareerSave(string path){PathName=path;}
        private static string Hash(string s){using(var sha=SHA256.Create())return BitConverter.ToString(sha.ComputeHash(Encoding.UTF8.GetBytes(s))).Replace("-","");}
        private CareerData Read(string path)
        {try{if(!File.Exists(path))return null;var raw=File.ReadAllText(path);if(raw.Length>65536)return null;var e=JsonUtility.FromJson<Envelope>(raw);if(e==null||e.version!=1||string.IsNullOrEmpty(e.payload)||e.checksum!=Hash(e.payload))return null;var d=JsonUtility.FromJson<CareerData>(e.payload);return CareerRules.IsValid(d)?d:null;}catch{return null;}}
        public CareerData Load()
        {Recovered=false;var d=Read(PathName);if(d!=null)return d;d=Read(PathName+".backup");if(d!=null){Recovered=true;return d;}Recovered=File.Exists(PathName);return new CareerData();}
        public bool Save(CareerData data)
        {
            if(!CareerRules.IsValid(data))return false;
            try{Directory.CreateDirectory(System.IO.Path.GetDirectoryName(PathName));string payload=JsonUtility.ToJson(data);string raw=JsonUtility.ToJson(new Envelope{payload=payload,checksum=Hash(payload)});File.WriteAllText(PathName+".pending",raw);if(Read(PathName)!=null)File.Copy(PathName,PathName+".backup",true);File.Copy(PathName+".pending",PathName,true);return Read(PathName)!=null;}catch(Exception e){Debug.LogWarning("Career save failed: "+e.GetType().Name);return false;}
        }
    }
}
