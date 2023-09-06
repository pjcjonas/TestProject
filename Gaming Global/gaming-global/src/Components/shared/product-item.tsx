import React from 'react';
import { Product } from '../products';

interface ProductItemProps extends Product{
    children?: React.ReactNode
}

export const ProductItem: React.FunctionComponent<ProductItemProps> = (props: ProductItemProps):JSX.Element => {
    return <React.Fragment>
        <div key={props.productID} style={{borderBottom: "1px solid black", margin: "20px", float: "left", maxWidth: "200px"}}>
            <img src={props.imageURL} alt={props.productName} />
            <strong>{props.productName}</strong>
            <p>{props.description}</p>
            <p>R{props.price}</p>
            {props.children}
        </div>
    </React.Fragment>    
}