using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRCloud.Public.templates
{
    class EmailTemplate
    {

        public string Udvozlo_Email(string name)
        {
            string content = @"
                                            <h2>Tisztelt "+ name + @"!</h2>
                                                   <p class=''>
                                                Köszönjük jelentkezését, kollegáink megkezdik pályázata feldolgozását.

                                                Amennyiben megtaláljuk az Ön számára alkalmas pozíciót, felvesszük Önnel a kapcsolatot.
                                            </p>
                                            <p>
                                                Felhívjuk figyelmét, hogy adatait 1 évig tároljuk adatbázisunkban.
                                            </p>
                                            <p Style='margin-bottom: 30px'>
                                                Amennyiben szeretné módosítani adatait vagy törölni jelentkezését, kérjük jelezze a privacy@phoenix-mecano.hu e-mail címen.
                                            </p>
                                            <p>Üdvözlettel,</p>
                                            <p>Phoenix Mecano Kecskemét Kft.</p>
                                            <p>Személyügyi Osztály</p></td>";
            return front + content + bottom;
        }
        public string Elutasito_Email(string name)
        {
            string content = @"
                                            <h2>Tisztelt " + name + @"!</h2>
                                                 <p  class=''>
                                                Köszönjük jelentkezését a Phoenix Mecano Kecskemét Kft-hez.
                                                Sajnálattal közöljük, hogy a megpályázott pozícióra nem került kiválasztásra.
                                                </p>
                                                <p>
                                                    Felhívjuk figyelmét, hogy adatait 1 évig tároljuk adatbázisunkban.
                                                </p>
                                                <p> Amennyiben szeretné módosítani adatait vagy törölni jelentkezését, kérjük jelezze a privacy@phoenix-mecano.hu e-mail címen.</p>
                                                <p Style='margin-bottom: 30px;'>További pályafutásához sok sikert kívánunk!</p>
                                                <p>Üdvözlettel,</p>
                                                <p>Phoenix Mecano Kecskemét Kft.</p>
                                                <p>Személyügyi Osztály</p>";
            return front + content + bottom;
        }




        public string Belsos_Meghivo_Email(string name, string projekt_name, string date, string helyszin, string jeloltnev)
        {
            string content = @"
                                            <h2>Tisztelt " + name + @"!</h2>
                                            <p Style='margin-bottom: 30px;' class=''>
                                                A következő interjúd időpontja <b>"+projekt_name+"</b> pozícióra:  <b>"+date+ @"</b>
                                                Jelentkező neve: <b>" + jeloltnev + @"</b>
                                                Helyszín: <b>" + helyszin + @"</b>
                                            </p>
                                            <p>Üdvözlettel,</p>
                                            <p>Phoenix Mecano Kecskemét Kft.</p>
                                            <p>Személyügyi Osztály</p>";
            return front + content + bottom;
        }
        public string Jelolt_Meghivo_Email(string name, string projekt_name, string date, List<string> resztvevok)
        {
            string resztvevok_layout = "";
            foreach (var item in resztvevok)
            {
                resztvevok_layout += item + ", ";
            }
            string content = @"
                                            <h2>Tisztelt " + name + @"!</h2>
                                            <p>Telefonos egyeztetésünkre hivatkozva szeretném megerősíteni a személyes találkozó időpontját (<b>" + projekt_name+ @"</b>) pozícióra történő meghallgatás kapcsán.</p>
                                            <p>Helyszín: <b>Phoenix Mecano Kecskemét Kft. <br>6000 Kecskemét, Szent István körút 24. </b></p>
                                            <p>Időpont: <b>" + date+ @"</b></p>
                                            <p>Résztvevők: <br>" + resztvevok_layout + @"</p>
                                            <p>A portán személyi igazolvány bemutatása szükséges.</p>
                                            <p>Az interjú időtartama kb. <b>1,5  órát</b> vesz igénybe, kérjük hogy a beléptetésre tekintettel az interjú kezdete előtt 15 perccel szíveskedjék megjelenni.</p>
                                            <p Style='margin-bottom: 30px;'>Várjuk a megbeszélt időpontban!</p>
                                            <p>Üdvözlettel,</p>
                                            <p>Phoenix Mecano Kecskemét Kft.</p>
                                            <p>Személyügyi Osztály</p>";
            return front + content + bottom;
        }



        string front = @"<html>
<head>
    <meta name='viewport' content='width=device-width' />
    <meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />
    <title>Simple Transactional Email</title>
    <style>
        img {
            border: none;
            -ms-interpolation-mode: bicubic;
            max-width: 100%;
            float: right;
        }

        body {
            background-color: #f6f6f6;
            font-family: sans-serif;
            -webkit-font-smoothing: antialiased;
            font-size: 14px;
            line-height: 1.4;
            margin: 0;
            padding: 0;
            -ms-text-size-adjust: 100%;
            -webkit-text-size-adjust: 100%;
        }

        table {
            border-collapse: separate;
            mso-table-lspace: 0pt;
            mso-table-rspace: 0pt;
            width: 100%;
        }

            table td {
                font-family: sans-serif;
                font-size: 14px;
                vertical-align: top;
            }

        .body {
            background-color: #f6f6f6;
            width: 100%;
        }

        .container {
            display: block;
            Margin: 0 auto !important;
            max-width: 580px;
            padding: 10px;
            width: 580px;
        }

        .content {
            box-sizing: border-box;
            display: block;
            Margin: 0 auto;
            max-width: 580px;
            padding: 10px;
        }

        .main {
            background: #ffffff;
            border-radius: 3px;
            width: 100%;
        }

        .wrapper {
            box-sizing: border-box;
            padding: 20px;
        }

        .content-block {
            padding-bottom: 10px;
            padding-top: 10px;
        }

        .footer {
            clear: both;
            Margin-top: 10px;
            text-align: center;
            width: 100%;
        }

            .footer td,
            .footer p,
            .footer span,
            .footer a {
                color: #999999;
                font-size: 12px;
                text-align: center;
            }

        h1,
        h2,
        h3,
        h4 {
            color: #000000;
            font-family: sans-serif;
            font-weight: 400;
            line-height: 1.4;
            margin: 0;
            Margin-bottom: 30px;
        }

        h1 {
            font-size: 35px;
            font-weight: 300;
            text-align: center;
            text-transform: capitalize;
        }

        p,
        ul,
        ol {
            font-family: sans-serif;
            font-size: 14px;
            font-weight: normal;
            line-height: 170%;
            margin: 0;
            Margin-bottom: 15px;
        }

            p li,
            ul li,
            ol li {
                list-style-position: inside;
                margin-left: 5px;
            }

        a {
            color: #3498db;
            text-decoration: underline;
        }

        .btn {
            box-sizing: border-box;
            width: 100%;
        }

            .btn > tbody > tr > td {
                padding-bottom: 15px;
            }

            .btn table {
                width: auto;
            }

                .btn table td {
                    background-color: #ffffff;
                    border-radius: 5px;
                    text-align: center;
                }

            .btn a {
                background-color: #ffffff;
                border: solid 1px #3498db;
                border-radius: 5px;
                box-sizing: border-box;
                color: #3498db;
                cursor: pointer;
                display: inline-block;
                font-size: 14px;
                font-weight: bold;
                margin: 0;
                padding: 12px 25px;
                text-decoration: none;
                text-transform: capitalize;
            }

        .btn-primary table td {
            background-color: #3498db;
        }

        .btn-primary a {
            background-color: #3498db;
            border-color: #3498db;
            color: #ffffff;
        }

        .last {
            margin-bottom: 0;
        }

        .first {
            margin-top: 0;
        }

        .align-center {
            text-align: center;
        }

        .align-right {
            text-align: right;
        }

        .align-left {
            text-align: left;
        }

        .clear {
            clear: both;
        }

        .mt0 {
            margin-top: 0;
        }

        .mb0 {
            margin-bottom: 0;
        }

        .preheader {
            color: transparent;
            display: none;
            height: 0;
            max-height: 0;
            max-width: 0;
            opacity: 0;
            overflow: hidden;
            mso-hide: all;
            visibility: hidden;
            width: 0;
        }

        .powered-by a {
            text-decoration: none;
        }

        hr {
            border: 0;
            border-bottom: 1px solid #f6f6f6;
            Margin: 20px 0;
        }

        @media only screen and (max-width: 620px) {
            table[class=body] h1 {
                font-size: 28px !important;
                margin-bottom: 10px !important;
            }

            table[class=body] p,
            table[class=body] ul,
            table[class=body] ol,
            table[class=body] td,
            table[class=body] span,
            table[class=body] a {
                font-size: 16px !important;
            }

            table[class=body] .wrapper,
            table[class=body] .article {
                padding: 10px !important;
            }

            table[class=body] .content {
                padding: 0 !important;
            }

            table[class=body] .container {
                padding: 0 !important;
                width: 100% !important;
            }

            table[class=body] .main {
                border-left-width: 0 !important;
                border-radius: 0 !important;
                border-right-width: 0 !important;
            }

            table[class=body] .btn table {
                width: 100% !important;
            }

            table[class=body] .btn a {
                width: 100% !important;
            }

            table[class=body] .img-responsive {
                height: auto !important;
                max-width: 100% !important;
                width: auto !important;
            }
        }

        @media all {
            .ExternalClass {
                width: 100%;
            }

                .ExternalClass,
                .ExternalClass p,
                .ExternalClass span,
                .ExternalClass font,
                .ExternalClass td,
                .ExternalClass div {
                    line-height: 100%;
                }

            .apple-link a {
                color: inherit !important;
                font-family: inherit !important;
                font-size: inherit !important;
                font-weight: inherit !important;
                line-height: inherit !important;
                text-decoration: none !important;
            }

            .btn-primary table td:hover {
                background-color: #34495e !important;
            }

            .btn-primary a:hover {
                background-color: #34495e !important;
                border-color: #34495e !important;
            }
        }
    </style>
</head>
<body class=''>
    <table border='0' cellpadding='0' cellspacing='0' class='body'>
        <tr>
            <td>&nbsp;</td>
            <td class='container'>
                <div class='content'>

                    <span class='preheader'>This is preheader text. Some clients will show this text as a preview.</span>
                    <table class='main'>
                        <tr>
                            <td class='wrapper'>
                                <table border='0' cellpadding='0' cellspacing='0' Style='padding: 10px;'>
                                    <img src='https://www.phoenix-mecano.hu/wp-content/uploads/2017/02/PMK1_CMYK-300x70.png' width='215' alt='Smiley face' height='50'>
                                    <tr>
                                        <td>
                                            <p></p>";
        string bottom = @"
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>

                    <div class='footer'>
                        <table border='0' cellpadding='0' cellspacing='0'>
                            <tr>
                                <td class='content-block'>
                                    <span class='apple-link'>Phoenix Mecano Kecskemét kft.</span>
                                </td>
                            </tr>
                            <tr>
                                <td class='content-block powered-by'>
                                    Szent István krt. 24, 6000
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>
</body>
</html>";
    }
}
