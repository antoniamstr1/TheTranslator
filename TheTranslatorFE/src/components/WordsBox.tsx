import "../style/WordsBox.css";
import { useState } from 'react';
import axios from "axios";


interface WordsProps {
    words: string[];
}
export default function WordsBox({ words }: WordsProps) {

    const [openButtons, setOpenButtons] = useState(() => Array(words.length).fill(false));

    const [detailsData, setDetailsData] = useState<(any | null)[]>(
        Array(words.length).fill(null)
    );
    const [error, setError] = useState<(string | null)[]>(Array(words.length).fill(null));

    const [loading, setLoading] = useState(() =>
        Array(words.length).fill(false)
    );
    const API_BASE_URL = "https://thetranslator.onrender.com/word-analysis/it/";

    const handleClick = async (i: number) => {
        setOpenButtons(prev => {
            const newArr = [...prev];
            newArr[i] = !newArr[i];
            return newArr;
        });

        if (!detailsData[i]) {
            setLoading(prev => {
                const newArr = [...prev];
                newArr[i] = true;
                return newArr;
            });
            setError(prev => {
                const newArr = [...prev];
                newArr[i] = null;
                return newArr;
            });

            try {
                const response = await axios.get(`${API_BASE_URL}${words[i]}`);
                setDetailsData(prev => {
                    const newArr = [...prev];
                    newArr[i] = response.data;
                    return newArr;
                });
            } catch (err: any) {
                setError(prev => {
                    const newArr = [...prev];
                    newArr[i] = err.message || "Error fetching data";
                    return newArr;
                });
            } finally {
                setLoading(prev => {
                    const newArr = [...prev];
                    newArr[i] = false;
                    return newArr;
                });
            }
        }
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
                                    {/* Example rendering of conjugations if available */}
                                    {detailsData[index]?.conjugations?.map((tense: any, tIdx: number) => (
                                        <div key={tIdx} className="conjugations-container">
                                            <div className="tense">{tense.tenseName}</div>
                                            <div className="verbs-container">
                                                {tense.forms.map((form: any, fIdx: number) => (
                                                    <div key={fIdx} className="verb-row">
                                                        <div className="verb-face">{form.person}</div>
                                                        <div className="verb-conjugated">{form.form}</div>
                                                    </div>
                                                ))}
                                            </div>
                                        </div>
                                    ))}
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