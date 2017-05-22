public abstract class PlatformPlugin {

    protected string listenerName;

    private PlatformPlugin() { }

    public PlatformPlugin(string listenerName) : this() {
        this.listenerName = listenerName;
    }

    protected abstract void SetListenerName(string name);

}