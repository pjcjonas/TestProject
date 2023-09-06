import React, { useEffect } from 'react';
import { UserContext } from '../Context/user-context';
import { services } from '../Services/API';
import { AccessToken } from './login';

const Home: React.FunctionComponent = ():JSX.Element => {
    const {user} = React.useContext(UserContext);
    useEffect(() => {
        if (user.jwtToken && user.jwtToken !== '') {
            services.verifySession(user.jwtToken || "").then((response: AccessToken) => {
                console.log(response);
            })
        }
    }, [])
    return <React.Fragment>
        <p>HOME</p>
    </React.Fragment>
}

export default Home