using AutoMapper;

namespace WebApi.Maps
{
    public class StringToEnumConverter<TEnum> : ITypeConverter<string, TEnum> where TEnum : struct
    {
        public TEnum Convert(string source, TEnum destination, ResolutionContext context)
        {
            if (Enum.TryParse(source, true, out TEnum result))
            {
                return result;
            }
            else
            {
                throw new ArgumentException($"Invalid value for enum {typeof(TEnum).Name}: {source}");
            }
        }
    }
}
