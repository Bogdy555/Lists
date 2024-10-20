let categoryCardClone = null;
let categoryButtonCancelClone = null;

function MakeEditCategory(button, password, name, description)
{
    if (categoryButtonCancelClone != null)
    {
        MakeCardCategory(categoryButtonCancelClone);
    }

    let parent = button.parentElement;
    categoryCardClone = parent.cloneNode(true);

    let form = document.createElement("form");

    let inputPassword = document.createElement("input");
    let inputName = document.createElement("input");
    let inputDescription = document.createElement("input");
    let buttonDone = document.createElement("button");
    let buttonCancel = document.createElement("button");
    categoryButtonCancelClone = buttonCancel;

    form.classList.add("vertical-container"); // class=""
    form.action = "/Categories/Edit/" + parent.children[0].children[1].innerHTML + "/";
    form.method = "post";

    inputPassword.classList.add("hidden-item");
    inputPassword.type = "password";
    inputPassword.name = "Password";
    inputPassword.setAttribute("value", password);

    inputName.classList.add("cute-subcard");
    inputName.classList.add("content-sizer");
    inputName.type = "text";
    inputName.name = "newCategory.Name";
    inputName.setAttribute("value", name);

    inputDescription.classList.add("cute-subcard");
    inputDescription.classList.add("content-sizer");
    inputDescription.type = "text";
    inputDescription.name = "newCategory.Description";
    inputDescription.setAttribute("value", description);

    buttonDone.classList.add("cute-subcard");
    buttonDone.classList.add("content-sizer");
    buttonDone.type = "submit";
    buttonDone.innerHTML = "Done";

    buttonCancel.classList.add("cute-subcard");
    buttonCancel.classList.add("content-sizer");
    buttonCancel.setAttribute("onclick", "MakeCardCategory(this)");
    buttonCancel.innerHTML = "Cancel";

    form.appendChild(inputPassword);
    form.appendChild(inputName);
    form.appendChild(inputDescription);
    form.appendChild(buttonDone);

    parent.children[0].remove();
    parent.children[0].remove();
    parent.children[0].remove();
    parent.children[0].remove();

    parent.appendChild(form);
    parent.appendChild(buttonCancel);
}

function MakeCardCategory(button)
{
    let parent = button.parentElement;

    parent.children[0].remove();
    parent.children[0].remove();

    parent.appendChild(categoryCardClone.children[0].cloneNode(true));
    parent.appendChild(categoryCardClone.children[1].cloneNode(true));
    parent.appendChild(categoryCardClone.children[2].cloneNode(true));
    parent.appendChild(categoryCardClone.children[3].cloneNode(true));

    categoryCardClone = null;
    categoryButtonCancelClone = null;
}



let itemCardClone = null;
let itemButtonCancelClone = null;

function MakeEditItem(button, categoryName, password, name, description)
{
    if (itemButtonCancelClone != null)
    {
        MakeCardItem(itemButtonCancelClone);
    }

    let parent = button.parentElement;
    itemCardClone = parent.cloneNode(true);

    let form = document.createElement("form");

    let inputPassword = document.createElement("input");
    let inputCategory = document.createElement("input");
    let inputName = document.createElement("input");
    let inputDescription = document.createElement("input");
    let buttonDone = document.createElement("button");
    let buttonCancel = document.createElement("button");
    itemButtonCancelClone = buttonCancel;

    form.classList.add("vertical-container"); // class=""
    form.action = "/Items/Edit/" + categoryName + "/" + parent.children[0].children[1].innerHTML + "/";
    form.method = "post";

    inputPassword.classList.add("hidden-item");
    inputPassword.type = "password";
    inputPassword.name = "Password";
    inputPassword.setAttribute("value", password);

    inputCategory.classList.add("hidden-item");
    inputCategory.type = "text";
    inputCategory.name = "newItem.CategoryName";
    inputCategory.setAttribute("value", categoryName);

    inputName.classList.add("cute-subcard");
    inputName.classList.add("content-sizer");
    inputName.type = "text";
    inputName.name = "newItem.Name";
    inputName.setAttribute("value", name);

    inputDescription.classList.add("cute-subcard");
    inputDescription.classList.add("content-sizer");
    inputDescription.type = "text";
    inputDescription.name = "newItem.Description";
    inputDescription.setAttribute("value", description);

    buttonDone.classList.add("cute-subcard");
    buttonDone.classList.add("content-sizer");
    buttonDone.type = "submit";
    buttonDone.innerHTML = "Done";

    buttonCancel.classList.add("cute-subcard");
    buttonCancel.classList.add("content-sizer");
    buttonCancel.setAttribute("onclick", "MakeCardItem(this)");
    buttonCancel.innerHTML = "Cancel";

    form.appendChild(inputPassword);
    form.appendChild(inputCategory);
    form.appendChild(inputName);
    form.appendChild(inputDescription);
    form.appendChild(buttonDone);

    parent.children[0].remove();
    parent.children[0].remove();
    parent.children[0].remove();
    parent.children[0].remove();

    parent.appendChild(form);
    parent.appendChild(buttonCancel);
}

function MakeCardItem(button)
{
    let parent = button.parentElement;

    parent.children[0].remove();
    parent.children[0].remove();

    parent.appendChild(itemCardClone.children[0].cloneNode(true));
    parent.appendChild(itemCardClone.children[1].cloneNode(true));
    parent.appendChild(itemCardClone.children[2].cloneNode(true));
    parent.appendChild(itemCardClone.children[3].cloneNode(true));

    itemCardClone = null;
    itemButtonCancelClone = null;
}



let subitemCardClone = null;
let subitemButtonCancelClone = null;

function MakeEditSubitem(button, itemName, itemCategoryName, password, name, description)
{
    if (subitemButtonCancelClone != null)
    {
        MakeCardSubitem(subitemButtonCancelClone);
    }

    let parent = button.parentElement;
    subitemCardClone = parent.cloneNode(true);

    let form = document.createElement("form");

    let inputPassword = document.createElement("input");
    let inputItem = document.createElement("input");
    let inputCategory = document.createElement("input");
    let inputName = document.createElement("input");
    let inputDescription = document.createElement("input");
    let buttonDone = document.createElement("button");
    let buttonCancel = document.createElement("button");
    subitemButtonCancelClone = buttonCancel;

    form.classList.add("vertical-container"); // class=""
    form.action = "/Subitems/Edit/" + itemName + "/" + itemCategoryName + "/" + parent.children[0].innerHTML + "/";
    form.method = "post";

    inputPassword.classList.add("hidden-item");
    inputPassword.type = "password";
    inputPassword.name = "Password";
    inputPassword.setAttribute("value", password);

    inputItem.classList.add("hidden-item");
    inputItem.type = "text";
    inputItem.name = "newSubitem.ItemName";
    inputItem.setAttribute("value", itemName);

    inputCategory.classList.add("hidden-item");
    inputCategory.type = "text";
    inputCategory.name = "newSubitem.ItemCategoryName";
    inputCategory.setAttribute("value", itemCategoryName);

    inputName.classList.add("cute-subcard");
    inputName.classList.add("content-sizer");
    inputName.type = "text";
    inputName.name = "newSubitem.Name";
    inputName.setAttribute("value", name);

    inputDescription.classList.add("cute-subcard");
    inputDescription.classList.add("content-sizer");
    inputDescription.type = "text";
    inputDescription.name = "newSubitem.Description";
    inputDescription.setAttribute("value", description);

    buttonDone.classList.add("cute-subcard");
    buttonDone.classList.add("content-sizer");
    buttonDone.type = "submit";
    buttonDone.innerHTML = "Done";

    buttonCancel.classList.add("cute-subcard");
    buttonCancel.classList.add("content-sizer");
    buttonCancel.setAttribute("onclick", "MakeCardSubitem(this)");
    buttonCancel.innerHTML = "Cancel";

    form.appendChild(inputPassword);
    form.appendChild(inputItem);
    form.appendChild(inputCategory);
    form.appendChild(inputName);
    form.appendChild(inputDescription);
    form.appendChild(buttonDone);

    parent.children[0].remove();
    parent.children[0].remove();
    parent.children[0].remove();
    parent.children[0].remove();

    parent.appendChild(form);
    parent.appendChild(buttonCancel);
}

function MakeCardSubitem(button)
{
    let parent = button.parentElement;

    parent.children[0].remove();
    parent.children[0].remove();

    parent.appendChild(subitemCardClone.children[0].cloneNode(true));
    parent.appendChild(subitemCardClone.children[1].cloneNode(true));
    parent.appendChild(subitemCardClone.children[2].cloneNode(true));
    parent.appendChild(subitemCardClone.children[3].cloneNode(true));

    subitemCardClone = null;
    subitemButtonCancelClone = null;
}
