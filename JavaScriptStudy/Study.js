function doAdd(iNum)
{
    alert(iNum + 100);
}

function doAdd(iNum)
{
    alert(iNum + 10);
    
}

function doAdd(iNum)
{
    if (arguments[0] < 9)
    {
        alert(arguments[0]);
    }
    if (arguments[0] > 9)
    {
        alert(">0");
    }
}

function CallAnotherMethod(otherMethod, vArgument)
{
    otherMethod(vArgument);
}

function otherMethod(message)
{
    alert(message);
}

function DisplayDetail()
{
    alert(doAdd.toString());
}
function CallFrameName(number)
{
    alert(window.frames[number].name);
    
    
}