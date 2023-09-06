export const getHeaders = () => {
    return { "Content-Type": "application/json", "Access-Control-Allow-Origin": "*" };
};
export const getAuthHeaders = (jwtToken: string) => {
    return { "Content-Type": "application/json", "Access-Control-Allow-Origin": "*", "Authorization": `Bearer ${jwtToken} ` }
};

export const asyncRequest = async (
    endpoint: string,
    requestBody: any,
    method?: "post" | "get",
    headers?: any
) => handleResponse(await apiServiceRequest(endpoint, requestBody, method, headers));

/**
 * Processes the http request to the resource
 * 
 * @param endpoint API endpoint 
 * @param requestBody The request body
 * @param method The request method - post || get
 * 
 * @returns Promise
 */

export const apiServiceRequest = async (
    endpoint: string,
    requestBody: any,
    method?: "post" | "get",
    headers?: any
): Promise<Response> => {
    console.log({
        method: method,
        headers: headers,
        body: requestBody ? JSON.stringify(requestBody) : undefined
    });
    const request: RequestInit = {
        method: method,
        headers: headers,
        body: requestBody ? JSON.stringify(requestBody) : undefined
    }

    const response = await fetch(endpoint, request);
    return response;
}

/**
 * Handles the response to make sure that the body is set to the 
 * correct structure in case of an error.
 * 
 * @param response 
 */
export async function handleResponse(response: Response) {
    if (response.status === 400 || response.status === 409) {
        throw await response.json();
    }

    if (!response.ok) {
        console.log("FALSE: handleResponse: ", response);
        throw Error(response.statusText);
    }

    return await response.json();
}

/**
 * API Services
 */
const apiUrl = "https://localhost:32770/";
export const services = {
    //getJwtToken: async (requestBody?: any) => await asyncRequest("https://localhost:32770/auth/authenticated", requestBody, "get"),
    verifySession: async (token: string) => await asyncRequest(`${apiUrl}auth/verify-session`, {}, "post", { "Content-Type": "application/json", "Access-Control-Allow-Origin": "*", "Authorization": `Bearer ${token} ` }),
    signInUser: async (requestBody: any) => await asyncRequest(`${apiUrl}auth/signin-user`, requestBody, "post", { "Content-Type": "application/json", "Access-Control-Allow-Origin": "*" }),
    getProducts: async () => await asyncRequest(`${apiUrl}products`, null, "get", { "Content-Type": "application/json", "Access-Control-Allow-Origin": "*" }),
    addToCart: async (token?: string, productId?: number) => await asyncRequest(`${apiUrl}products/add-to-cart`, {productId}, "post", { "Content-Type": "application/json", "Access-Control-Allow-Origin": "*", "Authorization": `Bearer ${token} ` }),
}