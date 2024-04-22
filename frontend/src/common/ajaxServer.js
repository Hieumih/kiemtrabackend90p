import $ from 'jquery';
import { getAPIUrl } from '@/common/config.js';

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

export const getLocations = async (options) => {
    var settings = {
        "url": apiUrl + "Locations",
        "method": "GET",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json",
        },
        "data": options,
    };
    
    return new Promise((resolve, reject) => {
        $.ajax(settings).done((response, textStatus, xhr) => {
            if (xhr.status === 200) {
                resolve(response);
            }
            else {
                reject(false);
            }
        }).fail(function (error) {
            reject(false);
        });
    });
        

};

export const addLocation = async (item) => {
    var settings = {
        "url": apiUrl + "Locations/add",
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json"
        },
        "data": JSON.stringify(item),
    };

    return new Promise((resolve, reject) => {
        $.ajax(settings).done((response, textStatus, xhr) => {
            if (xhr.status === 200) {
                resolve(true);
            }
            else {
                reject(false);
            }
        }).fail(function (error) {
            reject(false);
        });
    });


}

export const editLocation = async (item) => {
    var settings = {
        "url": apiUrl + "Locations/update",
        "method": "PUT",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json"
        },
        "data": JSON.stringify(item),
    };

    return new Promise((resolve, reject) => {
        $.ajax(settings).done((response, textStatus, xhr) => {
            if (xhr.status === 200) {
                resolve(true);
            }
            else {
                reject(false);
            }
        }).fail(function (error) {
            reject(false);
        });
    });

}

export const deleteLocation = async (item) => {
    var settings = {
        "url": apiUrl + "Locations/deleteOne",
        "method": "DELETE",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json"
        },
        "data": JSON.stringify(item),
    };

    return new Promise((resolve, reject) => {
        $.ajax(settings).done((response, textStatus, xhr) => {
            if (xhr.status === 200) {
                resolve(true);
            }
            else {
                reject(false);
            }
        }).fail(function (error) {
            reject(false);
        });
    });
}

export const deleteLocations = async (items) => {
    var settings = {
        "url": apiUrl + "Locations/deleteRange",
        "method": "DELETE",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json"
        },
        "data": JSON.stringify(items),
    };

    return new Promise((resolve, reject) => {
        $.ajax(settings).done((response, textStatus, xhr) => {
            if (xhr.status === 200) {
                resolve(true);
            }
            else {
                reject(false);
            }
        }).fail(function (error) {
            reject(false);
        });
    });
}

export const importLocations = async (items) => {
    var settings = {
        "url": apiUrl + "Locations/import",
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json"
        },
        "data": JSON.stringify(items),
    };

    return new Promise((resolve, reject) => {
        $.ajax(settings).done((response, textStatus, xhr) => {
            if (xhr.status === 200) {
                resolve(true);
            }
            else {
                reject(false);
            }
        }).fail(function (error) {
            reject(false);
        });
    });
}