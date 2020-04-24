# untold-garden
Main unity package for Untold Garden tools

Current tools:

**ErrorHandler**
* Use by calling ErrorHandler.Instance.Error([optional]string customError,[optional]int csvError)
* Custom error is any custom error message.
* Csv error is taking a message from a csv sheet. Add a textasset to Resources/UI called ErrorMessages and add messages to it.
* Same as above applies to warning messages, these appear on the bottom of the screen.
* Just press the error to close it.

