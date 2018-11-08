using SolidPrinciples.Model;

namespace SolidPrinciples.Hardware.Api
{
    public interface IFax
    {
        void Fax(Receipt receipt);
    }
}