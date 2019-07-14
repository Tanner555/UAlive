namespace Lasm.UAlive.Generation
{
    public static class CodeConverter
    {
        public static string AsString(AccessModifier scope)
        {
            var output = scope.ToString();

            if (scope == AccessModifier.PrivateProtected || scope == AccessModifier.ProtectedInternal) output = Patcher.AddLowerUpperNeighboringSpaces(output);

            return output.ToLower();
        }

        public static string AsString(RootAccessModifier scope)
        {
            return scope.ToString().ToLower();
        }
    }
}