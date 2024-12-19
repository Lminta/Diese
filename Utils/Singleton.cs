namespace Diese.Utils;

public abstract class Singleton<TClass> where TClass : Singleton<TClass>, new()
{
    static TClass? _instance;
    
    public static TClass Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new TClass();
            }
            return _instance;
        }
    }
    
    protected Singleton()
    {
        if (_instance != null)
        {
            throw new Exception("Singleton instance already exists!");
        }
    }
}