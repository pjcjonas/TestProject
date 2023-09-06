import React, { useEffect,useContext, useState } from 'react';
import { UserContext } from '../Context/user-context';
import { services } from '../Services/API';
import { AccessToken } from './login';
import { ProductItem } from './shared/product-item';

export interface Product {
    productID: number,
    categoryID: number,
    productName: string,
    description: string,
    price: number,
    imageURL: string,
}

export interface CartItem extends Product {
    userID: number,
    cartItemID: number
}

const Products: React.FunctionComponent = (): JSX.Element => {
    const { user } = useContext(UserContext);
    const [products, setProducts] = useState<Product[]>([]);
    const [addingToCart, setAddingToCart] = useState<boolean>(false);

    useEffect(() => {
        if (user.jwtToken && user.jwtToken !== '') {
            services.verifySession(user.jwtToken || "").then((response: AccessToken) => {
                console.log(response);
            })
        }
    }, [])

    useEffect(() => {
        services.getProducts().then((response: Product[]) => {
            setProducts(response)
        });
    }, [])

    const addToCart = (productID: number) => {
        if (user.jwtToken){
            setAddingToCart(true);
            services.addToCart(user.jwtToken, productID).then((response: CartItem[]) => {
                console.log(response);
                setAddingToCart(false);
            })
        }
    }

    return <React.Fragment>
        <p>PRODUCTS</p>
        {products.map((product: Product) => {
            return <ProductItem key={product.productID} {...product}>
                {user.jwtToken && <button onClick={() => addToCart(product.productID)}>Add to cart</button>}
                {!user.jwtToken && <p>Log in to add to cart</p>	}
            </ProductItem>
        })
        }
    </React.Fragment>
}

export default Products