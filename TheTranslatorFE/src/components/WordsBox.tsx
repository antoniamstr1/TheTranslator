import "../style/WordsBox.css";
import { useState } from 'react';
import axios from "axios";


interface WordsProps {
    words: string[];
    language: string;
}

interface ConjugationForm {
    type: string;
    value: string;
}

interface ConjugationPersons {
    [person: string]: ConjugationForm;
}

interface Conjugation {
    [tense: string]: ConjugationPersons;
}

interface Analysis {
    type: string;
    forms: {
        word: string;
        tags: string[];
    };
    definitions: string[];
}

interface WordDetails {
    analysis: Analysis;
    conjugation?: Conjugation;
}

export default function WordsBox({ words, language }: WordsProps) {

    const [openButtons, setOpenButtons] = useState(() => Array(words.length).fill(false));

    const [detailsData, setDetailsData] = useState<(WordDetails  | null)[]>(
        Array(words.length).fill(null)
    );
    const API_BASE_URL = `${import.meta.env.VITE_API_BASE_URL}/${language}/`;

    const handleClick = async (i: number) => {

        setOpenButtons(prev => {
            const newArr = [...prev];
            newArr[i] = !newArr[i];
            return newArr;
        });
        if (!detailsData[i]) {
            const response = await axios.get(`${API_BASE_URL}${words[i]}`);
            /* ŠTO AKO VRATI 400 -> ne može riješiti infinitiv */
            setDetailsData(prev => {
                const newArr = [...prev];
                newArr[i] = response.data;
                return newArr;
            });
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
                                        <div style={{ color: "gray" }}>{detailsData[index]?.analysis.type}</div>
                                        <div>
                                            {detailsData[index]?.analysis.definitions?.join(", ")}
                                        </div>
                                    </div>
                                </div>


                                {detailsData[index]?.conjugation && (
                                    <div className="conjugations-box">
                                        {Object.entries(detailsData[index].conjugation).map(([tenseName, persons], tIdx) => (
                                            <div key={tIdx} className="conjugations-container">
                                                <div className="tense">{tenseName}</div>
                                                <div className="verbs-container">
                                                    {Object.entries(persons).map(([person, form], fIdx) => (
                                                        <div key={fIdx} className="verb-row">
                                                            <div className="verb-face">{person}</div>
                                                            <div
                                                                className="verb-conjugated"
                                                                style={{ color: form.type !== "regular" ? "#c43838fe" : "black" }}
                                                            >
                                                                {form.value}
                                                            </div>
                                                        </div>
                                                    ))}
                                                </div>
                                            </div>
                                        ))}
                                    </div>
                                )}


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