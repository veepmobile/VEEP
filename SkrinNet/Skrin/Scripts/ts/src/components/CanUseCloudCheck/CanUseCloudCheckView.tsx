import * as React from "react";
import { IDispatch, IDispatchAny } from "../../models/Common"
import {getTerms} from './CloudTerms'

interface ICanUseCloudCheckViewProps {
    isShowing: boolean,
    action: any,
    accept: IDispatchAny,
    hideModal: IDispatch,
    checkCloudUsing: IDispatch
};

interface ICanUseCloudCheckViewState { };

class CanUseCloudCheckView extends React.Component<ICanUseCloudCheckViewProps, ICanUseCloudCheckViewState> {


    componentDidMount() {
        this.props.checkCloudUsing();
    }



    public render(): JSX.Element {

        if (!this.props.isShowing) {
            return null;
        }

        return (<div>
            <div className="modal fade in" style={{ display: "block" }}>
                <div className="modal-dialog">
                    <div className="modal-content">
                        <div className="modal-header"><button className="close" onClick={this.props.hideModal.bind(this)}>x</button> </div>
                        <div className="modal-body">
                            <h3>Для продолжения работы с сервисом "СКРИН Облако" просим принять условия пользовательского соглашения.</h3>                            
                                {getTerms()}
                            <div className="confirm-dialog">
                                <button className="btns darkblue" onClick={this.props.accept.bind(this, this.props.action)}>Принять</button>
                                <button className="btns grey" onClick={this.props.hideModal.bind(this)}>Отмена</button>
                            </div>
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

export default CanUseCloudCheckView;
