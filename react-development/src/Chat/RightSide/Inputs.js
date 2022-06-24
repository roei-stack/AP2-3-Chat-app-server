import {useEffect, useState} from 'react'
import * as U from '../../data/data'

function Inputs({ username, contactId, reload }) {

    const [answer, setAnswer] = useState(null);
    
    const sendText = async () => {
        // get the text to send
        let input = document.getElementById("text-input");
        if (!input.value) {
            return;
        }
        // sending message
        const response = await fetch(U.API_URL + '/api/contacts/' + contactId + '/messages?username=' + username, {
            method: 'POST',
            headers: {
                'accept': '*/*',
                'Content-Type': 'application/json'
            },
            body: '"' + input.value + '"'
        });
        input.value = '';
        setAnswer(response);
    }

    const keyDownEvent = (e) => {
        if (e.key === 'Enter') {
            sendText();
        }
    }

    useEffect(reload, [answer]);

    if (contactId) {
        return (
            <div className="input-msg-chat">
               
                <div className="text-input input-group mb-3">
                    <button onClick={sendText} className="btn btn-outline-secondary" type="button" id="button-addon2">
                        <i className="bi bi-send"></i>
                    </button>
                    <input id="text-input" className="form-control" type="text" placeholder="Write a message..." aria-label="Recipient's username" aria-describedby="button-addon2" onKeyDown={keyDownEvent}></input>
                </div>
            </div>
        );
    }
    return (
        <div></div>
    );
}


export default Inputs;