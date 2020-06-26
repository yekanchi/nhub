const fetcher = require("node-fetch");
test();



async function test() {
    let html: any;
    await fetcher("https://codal.ir/Reports/Decision.aspx?LetterSerial=ufihhs3RWUZQuVrEWpYnjA%3D%3D&rt=5&let=6&ct=0&ft=-1")
        .then(res =>
            res.text())
        .then(body => {
            // console.log(body);
            html = body;
        });

    console.log(html);
}