import "../style/WordsBox.css";
import { useState } from 'react';

interface WordsProps {
    words: string[];
}
export default function WordsBox({ words }: WordsProps) {

    const [openButtons, setOpenButtons] = useState(() => Array(words.length).fill(false));

    const handleClick = (i: number) => {
        setOpenButtons(prev => {
            const newArr = [...prev];
            newArr[i] = !newArr[i];
            return newArr;
        });
    };

    return (
        <div className="words-container">
            <div className="words">
                {words.map((word, index) => (
                    openButtons[index] ? (
                        <div key={index} className="expanded-row">
                            <div className="word-container">
                                <div className="details-container">
                                    <button
                                        className="word"
                                        onClick={() => handleClick(index)}
                                    >
                                        {word}
                                    </button>

                                    <div className="details-box">
                                        DETS
                                    </div>
                                </div>
                             
                                <div className="conjugations-box">
                                    {/* map po listi conjug */}
                                    <div className="conjugations-container">
                                        <div className="tense">tense</div>
                                        <div className="verbs-container">
                                            {/* map po licima */}
                                            <div className="verb-row">
                                                <div className="verb-face"></div>
                                                <div className="verb-conjugated"></div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                             
                        </div>
                    ) : (
                        <button
                            key={index}
                            className="word"
                            onClick={() => handleClick(index)}
                        >
                            {word}
                        </button>
                    )
                ))}
            </div>
        </div>
    )
}