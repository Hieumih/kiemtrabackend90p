

export const getAPIUrl = () => {
    console.log('if dev: ', import.meta.env.DEV)
    let api;
    if (import.meta.env.DEV) {
        api = "http://localhost:5139/api/";
    } else {
        api = "api/";
    }
    console.log('api: ', api)
    return api
}

export const childApiUrl = {
    location: {
        url : "locations/",
        add : "add",
        update: "update",
        delete: "delete",
        upload: "upload",
        deleteMultiple: "deleteMultiple",
        primarykey: "locationId"
    }
}

const apiUrl = "http://localhost:5139/api/";
export default apiUrl;

export const devKeyCode = 'k'