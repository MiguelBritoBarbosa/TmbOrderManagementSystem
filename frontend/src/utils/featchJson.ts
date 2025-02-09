export const fetchJson = async <T>(url: string, method: string, payload = {}): Promise<T> => {
    let requesConfig: RequestInit = {
        method,
        headers: new Headers({
            'Content-Type': 'application/json',
        }),
    };
    if (method !== 'GET' && method !== 'DELETE') {
        requesConfig.body = JSON.stringify(payload);
    }

    const data = await fetch(url, requesConfig);
    if (!data.ok) {
        console.log(data);
    }
    const jsonData = await data.json();
    return jsonData;
};
