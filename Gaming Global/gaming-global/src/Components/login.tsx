import React, { useEffect } from 'react';
import { useGoogleLogin } from '@react-oauth/google';
import { services } from '../Services/API';
import { UserContext } from '../Context/user-context';

export interface AccessToken {
    token: string;
}

const Login: React.FunctionComponent = (): JSX.Element => {

    const {user, setUserToken} = React.useContext(UserContext);

    useEffect(() => {
        if (user.jwtToken && user.jwtToken !== '') {
            services.verifySession(user.jwtToken || "").then((response: AccessToken) => {
                console.log(response);
            })
        }
    }, [])

    const login = useGoogleLogin({
        onSuccess: tokenResponse => {
            const {access_token} = tokenResponse;

            services.signInUser({access_token}).then((response: AccessToken) => {
                setUserToken(response.token);
            })
        },
        onError: error => {
            console.log(error);
        }
    })

    return <React.Fragment>
        <p>LOGIN</p>
        {user?.jwtToken === "" && <>
            <button onClick={() => login()}>
                Sign in with Google 
            </button>
        </>}
        {user?.jwtToken != "" && <>
            <p>Logged in</p>
        </>}
    </React.Fragment>
}

export default Login