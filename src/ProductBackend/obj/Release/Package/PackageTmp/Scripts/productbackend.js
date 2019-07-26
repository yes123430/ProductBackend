(function () {
    $(function () {
        /*  jQuery寫法測試練習
        $(function () {
            $("#dialog-modal").dialog({
                height: 140,
                modal: true
            });
        });

        $(document).ready(function () {
            $("#btnDelete").click(function () {
                console.log("TTT");
            });
        });
        */

        console.log('Go');
        var btnDefault = {
            'position': 'absolute',
            'right': '10px',
            'bottom': '10px',
        }
        SetShoppingCar();
        countShopCart();
        window.addEventListener('scroll', SetShoppingCar);

        $("#btnLogin").click(function () {
            if ($("#id").val() == "") {
                alert("你尚未填寫 帳號");
                eval("document.form1['id'].focus()");
            } else if ($("#ps").val() == "") {
                alert("你尚未填寫 密碼");
                $("#ps").focus();
            } else {
                document.form1.submit();
            }
        })

        $('#btnShopCart').click(function () {
            window.location.href = '/Shop/Amount'
        })

        //========== 後台 上傳照片 ============
        $("#imgInp").change(function () {
            readURL(this);
        });


        //========== 後台 新增產品 ============

        $("#addDataForm").submit(function (event) {
            /* event.preventDefault();*/
            var item = {};
            item['ProdName'] = $('#product-name').val();
            item['Price'] = $('#price-name').val();
            item['Count'] = $('#count-name').val();
            item['ProdDescription'] = $('#proddescription-name').val();
            console.log(item);

            $.ajax({
                type: 'post',
                url: '/Admin/Create',
                data: JSON.stringify(item),
                contentType: "application/json; charset=utf-8",
                beforeSend: function (xhr, opts) {
                    //show loading gif
                    $("#LoadingImage").show();
                },
                success: function (response) {

                    console.log('Close');
                },
                error: function () {

                },
                complete: function () {
                    //remove loading gif
                    $('#myModal').modal('in');
                }
            });
        });

    })

    //========== 修正購物車按鈕位置 ============
    let btnCShoppingCar = document.getElementById('CShoppingCar').style;
    let last_known_scroll_position = 10 + 'px';
    function SetShoppingCar() {
        let scrollY = window.scrollY;

        if (scrollY > 0) {
            last_known_scroll_position = 10 - scrollY
            btnCShoppingCar.bottom = last_known_scroll_position + 'px';
        }
        if (scrollY === 0) {
            btnCShoppingCar.bottom = '10px';
        }
    }
    function IniShoppingCar() {
        console.log(last_known_scroll_position);
        btnCShoppingCar.bottom = last_known_scroll_position;
    }

    function readURL(input) {
        if (input.files && input.files[0]) {
            /*藉由 FileReader 物件，Web 應用程式能以非同步（asynchronously）方式讀取儲存在用戶端的檔案（或原始資料暫存）內容，可以使用 File 或 Blob 物件指定要讀取的資料。*/
            var reader = new FileReader();
            reader.onload = function (e) {
                $('#blah').attr('src', e.target.result);
            }

            reader.readAsDataURL(input.files[0]);
        }
    }
})();

function JoinItemForShopCart(id, count) {

    var name = "shopcart";
    var item = [];
    item.push("ID=" + id);
    item.push("COUNT=" + count);

    let shopcart = readCookie(name);
    if (shopcart == null) {
        createCookie(name, item, 1, "/");
    }
    else {
        if (shopcart != null && shopcart != "") {
            let IsBool = new Boolean(false);
            var strArray = shopcart.split(',');
            for (i = 0; i < strArray.length; i = i + 2) {
                if (strArray[i].replace('ID=', '').toString() === id.toString()) {
                    strArray[i + 1] = 'COUNT=' + (parseInt(strArray[i + 1].replace('COUNT=', '')) + count);
                    IsBool = true;
                    break;
                }
            }
            if (IsBool == true) {
                createCookie(name, strArray, 1, "/");
                console.log(strArray);
                return;
            } 
        }
    }

    let str = "";
    if (shopcart != null && shopcart != "") str += shopcart + ",";
    str += item.toString();
    createCookie(name, str, 1, "/");
    console.log("有找到 : " + str);
    
    countShopCart();
}
function countShopCart() {
    let str = readCookie("shopcart");
    console.log("str : " + str);
    var btn = document.querySelector('#CShoppingCar button > span');
    if (btn != null && str != null && str != "") {
        btn.innerHTML = (str.split(',').length) / 2;
        return;
    } 
    if (btn != null) {
        btn.innerHTML = 0;
        console.log("近來等於零");
    } 
}
// 建立cookie
function createCookie(name, value, days, path) {
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        var expires = "; expires=" + date.toGMTString();
    }
    else var expires = "";
    document.cookie = name + "=" + value + expires + "; path=" + path;
}
//讀取cookie
function readCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}

function btnShopCartDeleteEvent(e, value) {
    console.log(e);
    console.log(value);
        
    var name = "shopcart";
    let strResult = "";
    let shopcart = readCookie(name);
    if (shopcart != null) {
        var strArray = shopcart.split(',');

        for (i = 0; i < strArray.length; i = i + 2) {
            var io = strArray[i].replace('ID=', '').toString();
            if (io === value.toString()) continue;
            if (strResult != "") strResult += ",";
            strResult += strArray[i] + "," + strArray[i + 1];
        }
        createCookie(name, strResult, 1, "/");

        $.ajax({
            type: 'post',
            url: '/Shop/CalAmount',
            dataType: 'json',
            data: { 'value': '' },
            async: false,
            success: function (response) {
                if (response.success) {
                    console.log("價格 : " + response.responseText);
                    document.querySelector('#ProdPriceAmount').innerHTML = response.responseText;
                }
            },
            error: function () {

            },
        })

        var row = e.parentNode.parentNode;
        row.parentNode.removeChild(row);
        countShopCart();

    }
}

function productBtnDeleteEvent(value) {
    $.ajax({
        type: 'post',
        url: "/Admin/Delete",
        dataType: 'json',
        data: {
            'value': value
        },
        success: function (response) {
            console.log('002');
            if (response.success) {
                window.location.replace("/Admin/Details");
            } else {
                alert(response.responseText);
            }
        },
        error: function () {

        },

    });
}