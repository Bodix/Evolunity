namespace Bodix.Evolunity.Utilities
{
	public interface IDataSerializer
	{
		string Extension { get; }
		void Serialize<T>(T data, string filePath);
		T Deserialize<T>(string filePath);
	}
}