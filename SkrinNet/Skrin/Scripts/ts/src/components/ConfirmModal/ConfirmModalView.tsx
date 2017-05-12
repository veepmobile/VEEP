import * as React from "react";
import {IDispatch,IDispatchAny} from "../../models/Common"

interface IConfirmModalViewProps {
    body:string,
    isShowing:boolean,
    action:any,
    accept:IDispatchAny,
    hideModal:IDispatch
};

interface IConfirmModalViewState {};

class ConfirmModalView extends React.Component<IConfirmModalViewProps, IConfirmModalViewState> {

    

    public render(): JSX.Element {

        if(!this.props.isShowing){
            return null;
        }

        var command_div=(<div className="confirm-dialog">
            <button className="btns darkblue" onClick={this.props.hideModal.bind(this)}>OK</button> 
        </div>);

        if(this.props.action!==null){
            command_div=(<div className="confirm-dialog">
            <button className="btns darkblue" onClick={this.props.accept.bind(this,this.props.action)}>Применить</button>
            <button className="btns grey" onClick={this.props.hideModal.bind(this)}>Отмена</button>
            </div>);
        }        

        return (<div>
                <div className="modal fade in" style={{display:"block"}}>
                    <div className="modal-dialog">
                        <div className="modal-content">
                            <div className="modal-header"><button className="close" onClick={this.props.hideModal.bind(this)}>x</button> </div>
                            <div className="modal-body">
                                <h3>{this.props.body}</h3>
                                {command_div}
                            </div>
                            <div className="modal-footer"></div>
                        </div>
                    </div>                            
                </div>
                <div className="modal-backdrop fade in">            
                </div>
                </div>  
        );
    }
}

export default ConfirmModalView;
