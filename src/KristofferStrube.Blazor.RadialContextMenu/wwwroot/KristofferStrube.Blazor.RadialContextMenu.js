var currentContextMenuElements;
var currentObjRef;

export function Init(ContextMenuElements, ObjRef) {
    currentContextMenuElements = ContextMenuElements;
    currentObjRef = ObjRef;
    document.addEventListener("mouseup", function (e) {
        const menuItemsIntersection = e.path.filter(value => currentContextMenuElements.includes(value));
        if (menuItemsIntersection.length == 0) {
            currentObjRef.invokeMethodAsync("Close");
        }
    });
}