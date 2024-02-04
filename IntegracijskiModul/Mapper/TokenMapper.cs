using IntegracijskiModul.BLModels;
using IntegracijskiModul.Modeli;

namespace IntegracijskiModul.Mapper
{
    public static class TokenMapper
    {
        // Iz modela u BL model
        public static IEnumerable<BLTokens> FromModelToBLModel(IEnumerable<Tokens> mt)
            => (IEnumerable<BLTokens>)mt.Select(t => FromModelToBLModel(t));

        private static object FromModelToBLModel(Tokens t)
        {
            return new BLTokens
            {
                Token = t.Token,
                RefreshToken = t.RefreshToken
            };
        }

        // obrnuto
        public static IEnumerable<Tokens> FromBLModelToModel(IEnumerable<BLTokens> blt)
            => (IEnumerable<Tokens>)blt.Select(c => FromBLModelToModel(c));

        private static object FromBLModelToModel(BLTokens c)
        {
            return new Tokens
            {
                Token = c.Token,
                RefreshToken = c.RefreshToken
            };
        }
    }
}
