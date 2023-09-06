import React, {useState} from 'react';

type User = {
    jwtToken?: string;
}

type UserContextType = {
    user: User;
    setUserToken: (token?: string) => void;
}

const defaultUserContext: UserContextType = {
    user: {jwtToken: ''},
    setUserToken: (token?: string) => {}
}


export const UserContext = React.createContext<UserContextType>(defaultUserContext);

interface UserProviderProps{
    children: React.ReactNode
}

export const UserProvider = ({children}: UserProviderProps ) => {
    const [user, setUser] = useState<User>({jwtToken: ''});
    const setUserToken = (token?: string) => setUser({jwtToken: token});
    return (
        <UserContext.Provider value={{user, setUserToken}}>
            {children}
        </UserContext.Provider>
    )
}