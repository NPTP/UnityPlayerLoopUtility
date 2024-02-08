# UnityPlayerLoopUtility
Small easy-to-use utility for hooking delegates into Unity PlayerLoop updates.

Import Unity Package Manager using this repo's git url: https://github.com/NPTP/UnityPlayerLoopUtility.git

Implement any of the interfaces that implement IPlayerLoopUser to hook into PlayerLoop.
Call the subscribe method to hook in, unsubscribe to unhook.
Fill out the virtual methods as desired.
Example in a non-monobehaviour class:

```
public class TestClass
{
    float secondsElapsed;

    public TestClass()
    {
        SubscribeToPlayerLoopUpdate();
    }
    
    public void PlayerLoopUpdate()
    {
        // Update logic goes here that will execute every frame, even in this non-MonoBehaviour class.
        // (You can use it in MonoBehaviours as well, if you really want to)
        secondsElapsed += Time.deltaTime;
    }
    
    public void Terminate()
    {
        // NOTE: It's highly recommended to tear down any usage by unsubscribing before
        // exiting play mode, as some PlayerLoop functionality may continue in edit mode.
        UnsubscribeFromPlayerLoopUpdate();
    }
}
```
This package is under the MIT License.