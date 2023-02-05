
namespace ReStore
{
    internal interface ICrud
    {
        void Create(int y);
        void Read(int y);
        void Update(int y);
        void Delete();
    }
}
