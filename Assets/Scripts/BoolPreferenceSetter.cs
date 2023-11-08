namespace Inscept.iOS.Settings.Examples
{
    public class BoolPreferenceSetter : PreferenceSetter<bool>
    {
        protected override bool GetValue(string key)
        {
            return PlayerPrefsExt.GetBool(key);
        }

        protected override void SetValue(string key, bool value)
        {
            PlayerPrefsExt.SetBool(key, value);
        }
    }
}