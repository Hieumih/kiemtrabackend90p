import $ from 'jquery';
import { getAPIUrl } from '~common/config.js';

const apiUrl = getAPIUrl();

// set token to cookie
// "expiration": "2024-04-18T09:53:06Z"
export const setToken = (token, expire_time) => {

}

// get token from cookie
export const getToken = () => {

}

export const check_login = async () => {
    var settings = {
        "url": apiUrl + "Account",
        "method": "GET",
        "timeout": 0,
        "headers": {
            "Authorization": "Bearer " + sessionStorage.getItem('token'),
        },
    };

    const byPassLogin = localStorage.getItem('byPassLogin') === 'true' && import.meta.env.DEV;
    if (byPassLogin) { 
        return new Promise((resolve, reject) => {
            resolve(true);
        });
    }

    return new Promise((resolve, reject) => {
        $.ajax(settings).done((data, textStatus, xhr) => {
            if (xhr.status === 200) {
                resolve(true);
            }
            else {
                resolve(false);
            }
        }).fail(function (error) {
            resolve(false);
        });
    });
}

export const login = (login_model) => {
    import.meta.env.DEV && console.log('Login', login_model);
    var settings = {
        "url": apiUrl + "Account/login",
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json",
        },
        "data": JSON.stringify({
            "email": login_model.user,
            "password": login_model.pass
        }),
    };

    // $.ajax(settings).done(function (response) {
    //     sessionStorage.setItem('token', response.token);
    // });

    return new Promise((resolve, reject) => {
        $.ajax(settings).done(function (response) {
            sessionStorage.setItem('token', response.token);
            resolve(true);
        }).fail(function (error) {
            resolve(false);
        });
    });
};

export const logout = () => {
    sessionStorage.removeItem('token');
    return true;
}