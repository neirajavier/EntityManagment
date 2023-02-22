<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MenuPrincipal.aspx.vb" Inherits="ArtemisAdmin.MenuPrincipal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="https://fonts.googleapis.com/css2?family=Montserrat:wght@300;400;500;600&display=swap" rel="stylesheet"/>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.0/dist/css/bootstrap.min.css" integrity="sha384-gH2yIJqKdNHPEq0n4Mqa/HGKIhSkIHeL5AyhkYV8i59U5AR6csBvApHHNl/vI1Bx" crossorigin="anonymous"/>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.0/dist/js/bootstrap.bundle.min.js" integrity="sha384-A3rJD856KowSb7dwlZdYEkO39Gagi7vIsF0jrRAoQmDKKtQBHUuLZ9AsSv4jD4Xa" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.0/dist/js/bootstrap.min.js" integrity="sha384-ODmDIVzN+pFdexxHEHFBQH3/9/vQ9uori45z4JjnFsRydbmQbmL5t1tQ0culUzyK" crossorigin="anonymous"></script>

    <link href="/Libs/leaflet/css/font-awesome.min.css" rel="stylesheet" />

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Menu Administración</title>

    <style type="text/css">
        .inp {
            position: relative;
            margin: auto;
            width: 100%;
            max-width: 280px;
            border-radius: 8px;
            overflow: hidden;
        }
        .inp .label {
            position: absolute;
            top: 20px;
            left: 12px;
            font-size: 16px;
            color: rgba(0, 0, 0, 0.5);
            font-weight: 500;
            transform-origin: 0 0;
            transform: translate3d(0, 0, 0);
            transition: all 0.2s ease;
            pointer-events: none;
        }
        .inp .focus-bg {
          position: absolute;
          top: 0;
          left: 0;
          width: 100%;
          height: 100%;
          background: rgba(0, 0, 0, 0.05);
          z-index: -1;
          transform: scaleX(0);
          transform-origin: left;
        }
        .inp input {
          -webkit-appearance: none;
          -moz-appearance: none;
               appearance: none;
          width: 100%;
          border: 0;
          font-family: inherit;
          padding: 16px 12px 0 12px;
          height: 56px;
          font-size: 16px;
          font-weight: 400;
          background: rgba(0, 0, 0, 0.02);
          box-shadow: inset 0 -1px 0 rgba(0, 0, 0, 0.3);
          color: #000;
          transition: all 0.15s ease;
        }
        .inp input:hover {
          background: rgba(0, 0, 0, 0.04);
          box-shadow: inset 0 -1px 0 rgba(0, 0, 0, 0.5);
        }
        .inp input:not(:-moz-placeholder-shown) + .label {
          color: rgba(0, 0, 0, 0.5);
          transform: translate3d(0, -12px, 0) scale(0.75);
        }
        .inp input:not(:-ms-input-placeholder) + .label {
          color: rgba(0, 0, 0, 0.5);
          transform: translate3d(0, -12px, 0) scale(0.75);
        }
        .inp input:not(:placeholder-shown) + .label {
          color: rgba(0, 0, 0, 0.5);
          transform: translate3d(0, -12px, 0) scale(0.75);
        }
        .inp input:focus {
          background: rgba(0, 0, 0, 0.05);
          outline: none;
          box-shadow: inset 0 -2px 0 #0077FF;
        }
        .inp input:focus + .label {
          color: #0077FF;
          transform: translate3d(0, -12px, 0) scale(0.75);
        }
        .inp input:focus + .label + .focus-bg {
          transform: scaleX(1);
          transition: all 0.1s ease;
        }

        .mytext {
            font: bold Monserrat;
            color: black;
        }
        #myUL {
          /* Remove default list styling */
          list-style-type: none;
          padding: 0;
          margin: 0;
        }

        #myUL li a {
          border: 1px solid #ddd; /* Add a border to all links */
          margin-top: -1px; /* Prevent double borders */
          background-color: #f6f6f6; /* Grey background color */
          padding: 12px; /* Add some padding */
          text-decoration: none; /* Remove default text underline */
          font-size: 18px; /* Increase the font-size */
          color: black; /* Add a black text color */
          display: block; /* Make it into a block element to fill the whole list */
        }

        #myUL li a:hover:not(.header) {
          background-color: #eee; /* Add a hover effect to all links, except for headers */
        }
    </style>
    <script type="text/javascript">
        function searchMenu() {
            // Declare variables
            var input, filter, ul, li, a, i, txtValue;
            input = document.getElementById('mySearch');
            filter = input.value.toUpperCase();
            ul = document.getElementById("grid");
            li = ul.getElementsByClassName('col-4');

            // Loop through all list items, and hide those who don't match the search query
            for (i = 0; i < li.length; i++) {
                a = li[i].getElementsByTagName("a")[0];
                txtValue = a.textContent || a.innerText;
                if (txtValue.toUpperCase().indexOf(filter) > -1) {
                    li[i].style.display = "";
                } else {
                    li[i].style.display = "none";
                }
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="row">
                <div class="col-lg-12">
                    <div class="page_title text-center mb-2">
                        <h3>M&oacute;dulo de Administraci&oacute;n</h3>
                    </div>
                </div>
                <hr/>
                <div class="row" style="padding-bottom:5%;">
                    <label for="inp" class="inp">
                      <input type="text" id="mySearch" onkeyup="searchMenu()" placeholder="Buscar opción.." style="width: 100%;"/>
                    </label>
                </div>
                <div id="grid" class="row">
                    <div class="row" style="padding-bottom:5%">
                        <div class="col-4" align="center">
                            <a href="#" style="text-decoration:none"><img src="../../images/car.png" style="width:60px; height:50px; padding:10px 10px 10px 10px"/>
                            <h4 class="mytext">Activos</h4></a>
                        </div>
                        <div class="col-4" align="center">
                            <a href="#" style="text-decoration:none"><img src="../../images/geocerca.png" style="width:60px; height:50px; padding:10px 10px 10px 10px"/>
                            <h4 class="mytext">Geocercas</h4></a>
                        </div>
                        <div class="col-4" align="center">
                            <a href="#" style="text-decoration:none"><img src="../../images/point.png" style="width:45px; height:50px; padding:10px 10px 10px 10px"/>
                            <h4 class="mytext">Puntos</h4></a>
                        </div>
                    </div>
                    <div class="row" style="padding-bottom:5%">
                        <div class="col-4" align="center">
                            <a href="#" style="text-decoration:none"><img src="../../images/conductor.png" style="width:50px; height:50px; padding:10px 10px 10px 10px"/>
                            <h4 class="mytext">Conductores</h4></a>
                        </div>
                        <div class="col-4" align="center">
                            <a href="#" style="text-decoration:none"><img src="../../images/keys.png" style="width:60px; height:50px; padding:10px 10px 10px 10px"/>
                            <h4 class="mytext">PassKey</h4></a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
