const express = require('express');
const app = express();

const fs = require('fs');
fs.readFile('password', 'utf8', function (err, data) {
    if (err) {
        return console.log(err);
    }
    config.password = data;
    console.log('Set password');
});

var Connection = require('tedious').Connection;
var Request = require('tedious').Request;
var config = {
    userName: 'catfin_user',
    password: 'haha no',
    server: 'db-personal-00.crjmlasrvtc6.us-west-2.rds.amazonaws.com',
    options: {database: 'catfin'}
};


function runSql(query, res) {
    console.log('Received query ' + query);
    var connection = new Connection(config);
    //connection.on('debug', function (err) {
    //    console.log('debug:', err);
    //});
    connection.on('connect', function (err) {
        if (err) {
            console.log(err);
        }
        // If no error, then good to proceed.
        executeStatement();
    });

    function executeStatement() {
        var result = "";
        request = new Request(query, function (err, rowCount) {
            if (err) {
                console.log(err);
            } else {
                if ('' == result) {
                    result = '{}'
                }
                console.log('Returning: ' + result);
                connection.close();
                //do response
                res.append('Content-Type', 'application/json');
                res.append('Access-Control-Allow-Origin', '*');
                res.write(result);
                res.end();
            }
        });
        request.on('row', function (columns) {
            columns.forEach(function (column) {
                result += column.value;
            });
        });
        connection.execSql(request);
    }
}

app.get('/goal', function (req, res) {
    runSql('exec dp_goal ' + req.query.user, res);
});

app.get('/cats', function (req, res) {
    runSql('exec dp_cats ' + req.query.user, res);
});

app.get('/transaction', function (req, res) {
    runSql('exec dp_transaction ' + req.query.user, res);
});

app.get('/category', function (req, res) {
    runSql('exec dp_category', res);
});

app.post('/create/user', function (req, res) {
    runSql('exec up_create_user ' + req.query.username, res);//
});

app.post('/create/goal', function (req, res) {
    runSql('declare @temp money = CAST(' + req.query.goalamt + ' as MONEY);exec up_create_goal ' + req.query.user + ',' + req.query.goalname + ',@temp', res);
});

app.post('/transaction', function (req, res) {
    runSql('declare @temp money = CAST(' + req.query.transamt + ' as MONEY);exec up_do_transaction ' + req.query.user + ',' + req.query.transname + ',@temp,' + req.query.transcat, res);
});


app.listen(8080, function () {
    console.log('Listening on port 8080');
});